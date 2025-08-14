using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.Grains.Filters
{
    public class LoggingOutgoingGrainLogFilter : IOutgoingGrainCallFilter
    {
        private ILogger _logger;

        public LoggingOutgoingGrainLogFilter(ILogger logger)
        {
            _logger = logger;
        }
        public async Task Invoke(IOutgoingGrainCallContext context)
        {
            _logger.LogInformation($"Outgoing silo grain filter: recieved on {context.Grain} {context.MethodName} method");

            await context.Invoke();
        }
    }
}
