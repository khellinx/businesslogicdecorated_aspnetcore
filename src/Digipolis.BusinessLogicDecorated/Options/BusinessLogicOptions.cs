using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.Options
{
    public class BusinessLogicOptions
    {
        private ILogger<BusinessLogicOptions> _logger;

        public BusinessLogicOptions(ILogger<BusinessLogicOptions> logger)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));

            _logger = logger;
        }

        public Func<IServiceProvider, object> DefaultAddOperatorFactory { get; set; }
    }
}
