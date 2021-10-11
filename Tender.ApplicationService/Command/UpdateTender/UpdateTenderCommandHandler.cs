namespace Tender.ApplicationService.Command.UpdateTender
{
    using CSharpFunctionalExtensions;

    using MediatR;

    using System.Diagnostics.Contracts;
    using System.Threading;
    using System.Threading.Tasks;

    using Tender.Infrastructure.Repositories.Interfaces;

    public class UpdateTenderCommandHandler: IRequestHandler<UpdateTenderCommand, Result>
    {
        private readonly ITenderQueryRepository tenderQueryRepository;

        public UpdateTenderCommandHandler(ITenderQueryRepository tenderQueryRepository)
        {
            this.tenderQueryRepository = tenderQueryRepository;
        }

        public async Task<Result> Handle(UpdateTenderCommand request, CancellationToken cancellationToken)
        {
            Contract.Assert(request != null);

            return await this.tenderQueryRepository.EditTender(request.UserId, request.Tender);
        }
    }
}
