using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.Grains.Filters
{
    public class LoggingFilter : IIncomingGrainCallFilter
    {
        private ILogger _logger;

        public LoggingFilter(ILogger logger)
        {
            _logger = logger;
        }

        public async Task Invoke(IIncomingGrainCallContext context)
        {
            _logger.LogInformation($"Incoming silo grain filter: recieved on {context.Grain} {context.MethodName} method");

            await context.Invoke();
        }
    }
}
