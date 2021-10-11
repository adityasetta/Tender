namespace Tender.ApplicationService.Command.PostTender
{
    using CSharpFunctionalExtensions;

    using MediatR;

    using Tender.Shared.Requests;

    public class PostTenderCommand : IRequest<Result>
    {
        public PostTenderCommand()
        {

        }

        public PostTenderCommand(string userId, PostTenderRequest tender) : this()
        {
            UserId = userId;
            Tender = tender;
        }

        public string UserId { get; set; }

        public PostTenderRequest Tender { get; set; }
    }
}
