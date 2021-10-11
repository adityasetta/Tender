namespace Tender.ApplicationService.Command.UpdateTender
{
    using CSharpFunctionalExtensions;

    using MediatR;

    using Tender.Shared.Requests;

    public class UpdateTenderCommand : IRequest<Result>
    {
        public UpdateTenderCommand()
        {

        }

        public UpdateTenderCommand(string userId, UpdateTenderRequest tender) : this()
        {
            UserId = userId;
            Tender = tender;
        }

        public string UserId { get; set; }

        public UpdateTenderRequest Tender { get; set; }
    }
}
