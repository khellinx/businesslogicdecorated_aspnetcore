using Digipolis.BusinessLogicDecorated.Inputs.Constraints;
using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.Preprocessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Decorators
{
    public class AsyncGetPreprocessingDecorator<TEntity, TInput> : AsyncGetDecorator<TEntity, TInput>
        where TInput : class, IHasIncludes<TEntity>
    {
        public AsyncGetPreprocessingDecorator(IAsyncGetOperator<TEntity, TInput> getOperator, IGetPreprocessor<TEntity, TInput> preprocessor) : base(getOperator)
        {
            Preprocessor = preprocessor;
        }

        public IGetPreprocessor<TEntity, TInput> Preprocessor { get; set; }

        public override Task<TEntity> GetAsync(int id, TInput input = null)
        {
            Preprocessor.Preprocess(input);

            return base.GetAsync(id);
        }
    }
}
