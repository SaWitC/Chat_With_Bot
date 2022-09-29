using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BotServer.Features.ValidationPipeline
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators,
            ILogger<ValidationBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var validationFailures = _validators
           .Select(validator => validator.Validate(request))
           .SelectMany(validationResult => validationResult.Errors)
           .Where(validationFailure => validationFailure != null)
           .ToList();

            if (validationFailures.Any())
            {
                _logger.LogInformation($"command or query have next exception {validationFailures.First()} and {validationFailures.Count - 1} more exceptions");
                throw new ValidationException(validationFailures);
            }

            return await next();
        }
    }
}
