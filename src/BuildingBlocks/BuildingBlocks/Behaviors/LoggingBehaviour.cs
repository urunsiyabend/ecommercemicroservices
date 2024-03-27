using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviors
{
    public class LoggingBehaviour<TRequest, TResponse>
        (ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("[START] Handling Request={Request} - Response={Response}",
                typeof(TRequest).Name, typeof(TResponse).Name);

            var timer = new Stopwatch();
            timer.Start();

            var response = await next();

            timer.Stop();
            var elapsedMilliseconds = timer.ElapsedMilliseconds;

            logger.LogInformation("[PERFORMANCE] Request={Request} - Response={Response} - ElapsedMilliseconds={ElapsedMilliseconds}",
                               typeof(TRequest).Name, typeof(TResponse).Name, elapsedMilliseconds);

            return response;
        }
    }
}
