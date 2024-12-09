using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Common.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TResponse : notnull
        where TRequest : notnull, IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("[START] Handler request = {request} - Response = {response} - RequestData = {requestData}",
                typeof(TRequest).Name, typeof(TResponse).Name, request);

            var timer = new Stopwatch();
            timer.Start();
            var response = await next();

            timer.Stop();
            var timeTaken = timer.Elapsed;
            if(timeTaken.Seconds > 3)
            {
                logger.LogWarning("[PERFORMANCE] The request {request} took {timeTaken} seconds",
                    typeof(TRequest).Name, timeTaken.Seconds);
            }

            logger.LogInformation("[END] Handled {request} with {response}",
                typeof(TRequest).Name, typeof(TResponse).Name);
            
            return response;
        }
    }
}
