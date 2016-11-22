using Digipolis.BusinessLogicDecorated.Inputs.Constraints;
using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.Preprocessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Decorators
{
    public class AsyncQueryPreprocessingDecorator<TEntity, TInput> : AsyncQueryDecorator<TEntity, TInput>
        where TInput : class, IHasIncludes<TEntity>, IHasFilter<TEntity>, IHasOrder<TEntity>
    {
        public AsyncQueryPreprocessingDecorator(IAsyncQueryOperator<TEntity, TInput> queryOperator, IQueryPreprocessor<TEntity, TInput> preprocessor) : base(queryOperator)
        {
            Preprocessor = preprocessor;
        }

        public IQueryPreprocessor<TEntity, TInput> Preprocessor { get; set; }

        public override Task<IEnumerable<TEntity>> QueryAsync(TInput input = null)
        {
            Preprocessor.Preprocess(input);

            return base.QueryAsync(input);
        }
    }
}
