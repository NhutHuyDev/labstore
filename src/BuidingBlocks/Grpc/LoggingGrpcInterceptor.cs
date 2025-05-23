using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuidingBlocks.Grpc
{
    public class LoggingGrpcInterceptor(ILogger<LoggingGrpcInterceptor> logger)
        : Interceptor
    {
        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            logger.LogInformation("[START] Handle Request={Request} - Response={Response} - RequestData={RequestData}",
                typeof(TRequest).Name, typeof(TResponse).Name, request
            );

            var timer = new Stopwatch();
            timer.Start();

            var reponse = await continuation(request, context);

            timer.Stop();
            var timeTaken = timer.Elapsed;
            if (timeTaken.Seconds > 3)
            {
                logger.LogWarning("[PERFORMANCE] The request {Request} took {TimeTaken}",
                        typeof(TRequest).Name, timeTaken.Seconds
                    );
            }

            logger.LogInformation("[END] Handle {Request} with {Response}", typeof(TRequest).Name, typeof(TResponse).Name);

            return reponse;
        }
    }
}
