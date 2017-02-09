# Business Logic Decorated Toolbox

Add a business logic layer to your solution, decorated style so it's easy to extend and test!

## Table of Contents

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->

- [Disclaimer](#disclaimer)
- [What and Why?](#whatandwhy)
- [Dictionary](#dictionary)
- [Minimal usage](#minimalusage)
- [Added value](#addedvalue)
- [CRUD operators](#crudoperators)
- [What else?](#whatelse)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

## DISCLAIMER

This is still a beta package and is currently being developed.
Use at your own risk.

Testing and documentation is absent/minimal but will be expanded in the future.

## What and Why?

Business logic is supposed be doing things, most of the times it's about *how data can be created, stored, and changed* ([Wikipedia](https://en.wikipedia.org/wiki/Business_logic)).
A lot of times, that happens in some particular way depending on technical dependencies, enterprise rules, personal taste, ...

However, things tend to be repeated for different entities. The flow might be the same independent of the type of entity, which could include:
- Retrieving, creating, storing or changing related entities
- Filtering
- Sorting
- Validation
- Wrapping things in a transaction
- ...

Some libraries use inheritance to solve the problem of code repetition ([DRY principle](https://en.wikipedia.org/wiki/Don't_repeat_yourself)). However, in our opinion, that's less flexible and could be harder to unit test.

We've chosen the [decorator pattern](https://en.wikipedia.org/wiki/Decorator_pattern) for our approach to endorse [SOLID](https://en.wikipedia.org/wiki/SOLID_(object-oriented_design)) code:
- Each class should just do *one thing, and one thing only*: [Single responsability](https://en.wikipedia.org/wiki/Single_responsibility_principle)
- Logic is easy to extend for all those special cases: [Open-closed principle](https://en.wikipedia.org/wiki/Open/closed_principle)
- High flexibility without overriding things or implementing things you don't need for your entity: [Interface segregation](https://en.wikipedia.org/wiki/Interface_segregation_principle)
- Highly decoupled logic in which also order of execution is configurable: [Dependency inversion](https://en.wikipedia.org/wiki/Dependency_inversion_principle)

## Dictionary

Operators, validators and pre- and postprocessors are the core of this project.  
Say what? That sounds quite generic!  
Yeah, it's because they are! Here we go:
- Operators: These contain the flow that is shared across (most) entities. At the moment, we've included an (async) interface for each CRUD operation:
  - Create: `IAsyncAddOperator`
  - Read: `IAsyncGetOperator` and `IAsyncQueryOperator`
  - Update: `IAsyncUpdateOperator`
  - Delete: `IAsyncDeleteOperator`
- Preprocessors: Execute some logic before the operator, e.g.:
  - Build a filtering or sorting predicate to be used by your favourite ORM,
  - Modify some related entities,
  - ...
- Postprocessors: Execute some logic after the operator, e.g.:
  - Commit the transaction,
  - Do some extra filtering or sorting outside of your ORM,
  - Merge entities,
  - ...
- Validators: Well, it's quite obvious what these do, I guess? :bowtie:

## Installation

Yay! You've come as far to actually give this package a try?  
To add the toolbox to a project, you add the package to the project.json:

``` json 
"dependencies": {
    "Digipolis.BusinessLogicDecorated":  "0.1.6"
 }
```

In Visual Studio you can also use the NuGet Package Manager to do this.

## Minimal usage

The bare minimum you could do is create one operator, e.g. the AddOperator for the Person entity:
``` csharp
public class PersonAsyncAddOperator : IAsyncAddOperator<Person>
{
    private MyContext _context;

    public PersonAsyncAddOperator(MyContext context)
    {
        _context = context;
    }

    public async Task<Person> AddAsync(Person entity, object input = null)
    {
        _context.People.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}
```

Next, specify the operator in your DI-container, using the OperatorBuilder.  
For asp.net core projects, this might be done in the ConfigureServices method of the startup class:

``` csharp
public void ConfigureServices(IServiceCollection services)
{
    ...

    var operatorBuilder = new OperatorBuilder();
    operatorBuilder.ConfigureAsyncAddOperator<Person>(serviceProvider => serviceProvider.GetService<PersonAddOperator>());
    operatorBuilder.AddOperators(services);

    ...
}
```

Now the operator can be instantiated with constructor injection in an Api controller:

``` csharp
[Route("api/[controller]")]
public class PeopleController : Controller
{
    // POST api/people
    [HttpPost]
    public void Post([FromServices]IAsyncAddOperator<Person> op, [FromBody]Person value)
    {
        var result = op.AddAsync(value);
        return Created(...);
    }
}
```

## Added value

Well, that's kinda basic, isn't it?  
Yeah, it's also the bare minimum.

Let's get to where the real added value of this package lies. Let's assume we want to:
- Do some validation before we save our new Person,
- Assign a random guid to the new entity,
- Alert an external system that a new person has been added.

Easy as pie!

We create a Validator class by implementing the IAddValidator interface:

``` csharp
public class PersonValidator : IAddValidator<Person>
{
    public void ValidateForAdd(Person entity, object input = null)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));
        if (string.IsNullOrEmpty(entity.Name))
            throw new ArgumentNullException(nameof(entity.Name));
    }
}
```

Next, we create a preprocessor that will generate the guid by implementing the IAddPreprocessor interface:
``` csharp
public class PersonGuidPreprocessor : IAddPreprocessor<Person>
{
    public void PreprocessForAdd(ref Person entity, ref object input)
    {
        entity.Guid = Guid.NewGuid();
    }
}
```

To inform the external system, we create a postprocessor which implements the IPostprocessor interface:
``` csharp
public class PersonAlertPostprocessor : IAddPostprocessor<Person>
{
    private IExternalSystem _externalSystem;

    public PersonPostprocessor(IExternalSystem externalSystem)
    {
        _externalSystem = externalSystem;
    }

    public void PostprocessForAdd(Person entity, object input, ref Person result)
    {
        _externalSystem.Alert("A new person has been added.");
    }
}
```

At last, we just register those classes in our OperatorBuilder.
Instead of only registering the operator, we tell it has a validator, a pre- and a postprocessor:
``` csharp
operatorBuilder.ConfigureAsyncAddOperator<Person>(serviceProvider => serviceProvider.GetService<PersonAddOperator>())
    .WithValidation<PersonValidator>()
    .WithPreprocessor<PersonGuidPreprocessor>()
    .WithPostprocessor<PersonAlertPostprocessor>();
```

The good thing is, the controller doesn't even need an update!  
You can even add as many validators and processors as you want.

Now imagine how unorganized this would look in an inheritance-based logic structure or how big your controller actions could've gotten when combining all this logic.

And the best thing is that all your classes are very clean, small, easy to test and just have a single responsability.
How far you're willing to go is up to you.

## CRUD operators

Now you're probably thinking:
If I need to add an operator for each crud operation for each of my entities, that's gonna be a huge dependency injection method/class!  
We've got your back! We developers are all lazy and that's a good thing, right?

Remember we told you the operators should contain the 'shared' flow for all/most entities? Generics for the win!
We added a way to define default operators, like this:

``` csharp
operatorBuilder.SetDefaultAsyncGetOperatorTypes(typeof(AsyncGetOperator<>), typeof(AsyncGetOperator<,>));
operatorBuilder.SetDefaultAsyncQueryOperatorTypes(typeof(AsyncQueryOperator<>), typeof(AsyncQueryOperator<,>));
operatorBuilder.SetDefaultAsyncAddOperatorTypes(typeof(AsyncAddOperator<>), typeof(AsyncAddOperator<,>));
operatorBuilder.SetDefaultAsyncUpdateOperatorTypes(typeof(AsyncUpdateOperator<>), typeof(AsyncUpdateOperator<,>));
operatorBuilder.SetDefaultAsyncDeleteOperatorTypes(typeof(AsyncDeleteOperator<>), typeof(AsyncDeleteOperator<,>));
```

All you need to do next, is to define crud controllers like this for each entity and all specified default operators will be configured for you:

``` csharp
operatorBuilder.ConfigureAsyncCrudOperators<Person>();
```

And yes, the crud collection operator configuration also supports adding validators and processors:

``` csharp
operatorBuilder.ConfigureAsyncCrudOperators<Person>()
    .WithValidation<PersonValidator>()
    .WithPreprocessor<PersonGuidPreprocessor>()
    .WithPostprocessor<PersonAlertPostprocessor>();
```

The operator builder checks which interface your validator or processor implements and wraps it around the corresponding operators.  
E.g.: If you want the update validation in the same `PersonValidator`, just let it implement the `IUpdateValidator<Person>` interface and the operator builder will do the rest.

And for really special entities (aren't they the best?), we've implemented an option to use a custom operator if the default ones don't suit your needs:
``` csharp
operatorBuilder.ConfigureAsyncCrudOperators<SpecialType>()
    .WithCustomOperator<SpecialTypeCustomOperator>();
```

The	same goes for this one. The operator builder checks which operator interfaces it implements and configures the corresponding operations.

## What else?

Well, there are a ton of other features:
- Get input
- Query input
- Custom input
- Custom id types (who uses integers for id anyway?)

I suggest you to check out the different samples to really get to know this amazing piece of bits and bytes (well, we wrote it, how could we not love it?).

## Todo's

- Testing
- Documentation
- Playing with covariant (out) and contravariant (in) generic modifiers to see if code can be re-used instead of repeated in places where TGetInput and TQueryInput are used.
- Add processor with generic output (e.g. for committing transactions regardless the type of entity)