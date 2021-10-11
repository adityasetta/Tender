namespace Tender.Api.Controllers
{
    using CSharpFunctionalExtensions;

    using FluentValidation;
    using FluentValidation.AspNetCore;
    using FluentValidation.Results;

    using MediatR;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using System.Collections.Generic;
    using System.Linq;

    using ValidationContext = FluentValidation.ValidationContext<object>;

    /// <summary>
    /// Validation controller to perform validations
    /// </summary>
    /// <typeparam name="TController">The type of the controller.</typeparam>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    public class ValidationController<TController> : ControllerBase
    {
        /// <summary>
        /// The validation helpers.
        /// </summary>
        private readonly IEnumerable<IValidator> validators;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<TController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationController{TController}"/> class.
        /// </summary>
        /// <param name="validators">The validation helpers.</param>
        /// <param name="logger">The logger.</param>
        public ValidationController(IEnumerable<IValidator> validators, ILogger<TController> logger)
        {
            this.validators = validators;
            this.logger = logger;
        }

        /// <summary>
        /// Validates the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Validation result</returns>
        protected Result Validate(IBaseRequest request)
        {
            this.logger.LogTrace("Validating the query : {query} ", request);
            var validationResult = this.GetValidationResult(request);
            if (validationResult == null || validationResult.IsValid)
            {
                return Result.Success();
            }

            validationResult.AddToModelState(this.ModelState, null);
            return Result.Failure("Validation errors found");
        }

        /// <summary>
        /// Gets the validation result.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The validation result.</returns>
        private ValidationResult GetValidationResult(IBaseRequest request)
        {
            if (request == null)
            {
                return new ValidationResult(new[] { new ValidationFailure("Request", "Request cannot be null") });
            }

            var context = new ValidationContext(request);
            return this.validators
                .Where(x => x.CanValidateInstancesOfType(request.GetType()))
                   .Select(v => v.Validate(context))
                   .FirstOrDefault();
        }
    }
}
