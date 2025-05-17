using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuidingBlocks.Behaviors
{
    public class LoggingBehavior<TRequest, TReponse>
        (ILogger<LoggingBehavior<TRequest, TReponse>> logger)
        : IPipelineBehavior<TRequest, TReponse>
        where TRequest : notnull, IRequest<TReponse>
        where TReponse : notnull
    {
        public async Task<TReponse> Handle(TRequest request, RequestHandlerDelegate<TReponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("[START] Handle Request={Request} - Response={Response} - RequestData={RequestData}",
                    typeof(TRequest).Name, typeof(TReponse).Name, request
                );

            var timer = new Stopwatch();
            timer.Start();

            var response = await next();

            timer.Stop();
            var timeTaken = timer.Elapsed;
            if (timeTaken.Seconds > 3) 
            {
                logger.LogWarning("[PERFORMANCE] The request {Request} took {TimeTaken}",
                        typeof(TRequest).Name, timeTaken.Seconds
                    );
            }

            logger.LogInformation("[END] Handle {Request} with {Response}",
                typeof(TRequest).Name, typeof(TReponse).Name);

            return response;
        }
    }
}
