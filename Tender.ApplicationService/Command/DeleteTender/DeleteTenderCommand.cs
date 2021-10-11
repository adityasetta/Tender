namespace Tender.ApplicationService.Command.DeleteTender
{
    using CSharpFunctionalExtensions;

    using MediatR;

    public class DeleteTenderCommand : IRequest<Result>
    {
        public DeleteTenderCommand()
        {

        }

        public DeleteTenderCommand(string userId, long tenderId) : this()
        {
            UserId = userId;
            TenderId = tenderId;
        }

        public string UserId { get; set; }

        public long TenderId { get; set; }
    }
}
