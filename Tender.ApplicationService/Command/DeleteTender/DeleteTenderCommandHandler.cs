namespace Tender.ApplicationService.Command.DeleteTender
{
    using CSharpFunctionalExtensions;

    using MediatR;

    using System.Diagnostics.Contracts;
    using System.Threading;
    using System.Threading.Tasks;

    using Tender.Infrastructure.Repositories.Interfaces;

    public class DeleteTenderCommandHandler: IRequestHandler<DeleteTenderCommand, Result>
    {
        private readonly ITenderQueryRepository tenderQueryRepository;

        public DeleteTenderCommandHandler(ITenderQueryRepository tenderQueryRepository)
        {
            this.tenderQueryRepository = tenderQueryRepository;
        }

        public async Task<Result> Handle(DeleteTenderCommand request, CancellationToken cancellationToken)
        {
            Contract.Assert(request != null);

            return await this.tenderQueryRepository.DeleteTender(request.UserId, request.TenderId);
        }
    }
}
