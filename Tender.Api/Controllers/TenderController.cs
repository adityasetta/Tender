namespace Tender.Api.Controllers
{
    using FluentValidation;

    using MediatR;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Tender.ApplicationService.Command.DeleteTender;
    using Tender.ApplicationService.Command.PostTender;
    using Tender.ApplicationService.Command.UpdateTender;
    using Tender.ApplicationService.Interfaces;
    using Tender.Libraries.UserAuthorization.Filters;
    using Tender.Shared.Entities;
    using Tender.Shared.Requests;
    using Tender.Shared.Responses;

    [ApiController]
    public class TenderController : ValidationController<TenderController>
    {
        private readonly IMediator _mediator;

        private readonly ILogger<TenderController> _logger;

        private readonly ITenderService _tenderService;

        public TenderController(ILogger<TenderController> logger, ITenderService tenderService, IMediator mediator,
                                IEnumerable<IValidator> validators) : base(validators, logger)
        {
            _logger = logger;
            _tenderService = tenderService;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("api/tender/{tenderId}")]
        [AuthorizeRoles(Role.Guest, Role.Admin)]
        public async Task<ActionResult<TenderDetailResponse>> GetTenderDetailAsync(long tenderId)
        {
            return await _tenderService.GetTenderDetail(tenderId).ConfigureAwait(false);
        }

        [HttpGet]
        [Route("api/tender")]
        [AuthorizeRoles(Role.Guest, Role.Admin)]
        public async Task<ActionResult<List<TenderListResponse>>> GetTenderListAsync()
        {
            return await _tenderService.GetTenderList().ConfigureAwait(false);
        }

        [HttpPost]
        [Route("api/tender/{userId}")]
        [AuthorizeRoles(Role.Admin)]
        public async Task<ActionResult> AddTenderAsync(string userId, [FromBody] PostTenderRequest request)
        {
            try
            {
                var command = new PostTenderCommand(userId, request);

                var validationResult = this.Validate(command);
                if (validationResult.IsFailure)
                {
                    return this.BadRequest(this.ModelState);
                }

                var result = await _mediator.Send(command).ConfigureAwait(false);
                if (result.IsFailure)
                {
                    return this.Ok(SubmissionResponse.ForFailure(result.Error));
                }

                return this.Ok();
            }
            catch (Exception ex)
            {
                return this.Ok(SubmissionResponse.ForFailure(ex.Message));
            }
        }

        [HttpPatch]
        [Route("api/tender/{userId}")]
        [AuthorizeRoles(Role.Admin)]
        public async Task<ActionResult> EditTenderAsync(string userId, [FromBody] UpdateTenderRequest request)
        {
            try
            {
                var command = new UpdateTenderCommand(userId, request);

                var validationResult = this.Validate(command);
                if (validationResult.IsFailure)
                {
                    return this.BadRequest(this.ModelState);
                }

                var result = await _mediator.Send(command).ConfigureAwait(false);
                if (result.IsFailure)
                {
                    return this.Ok(SubmissionResponse.ForFailure(result.Error));
                }

                return this.Ok();
            }
            catch (Exception ex)
            {
                return this.Ok(SubmissionResponse.ForFailure(ex.Message));
            }
        }

        [HttpDelete]
        [Route("api/tender/{userId}/{tenderId}")]
        [AuthorizeRoles(Role.Admin)]
        public async Task<ActionResult> DeleteTenderAsync(string userId, long tenderId)
        {
            try
            {
                var command = new DeleteTenderCommand(userId, tenderId);

                var validationResult = this.Validate(command);
                if (validationResult.IsFailure)
                {
                    return this.BadRequest(this.ModelState);
                }

                var result = await _mediator.Send(command).ConfigureAwait(false);
                if (result.IsFailure)
                {
                    return this.Ok(SubmissionResponse.ForFailure(result.Error));
                }

                return this.Ok();
            }
            catch (Exception ex)
            {
                return this.Ok(SubmissionResponse.ForFailure(ex.Message));
            }
        }
    }
}
