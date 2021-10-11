namespace Tender.ApplicationService.Command.PostTender
{
    using CSharpFunctionalExtensions;

    using MediatR;

    using System.Diagnostics.Contracts;
    using System.Threading;
    using System.Threading.Tasks;

    using Tender.Infrastructure.Repositories.Interfaces;

    public class PostTenderCommandHandler: IRequestHandler<PostTenderCommand, Result>
    {
        private readonly ITenderQueryRepository tenderQueryRepository;

        public PostTenderCommandHandler(ITenderQueryRepository tenderQueryRepository)
        {
            this.tenderQueryRepository = tenderQueryRepository;
        }

        public async Task<Result> Handle(PostTenderCommand request, CancellationToken cancellationToken)
        {
            Contract.Assert(request != null);

            return await this.tenderQueryRepository.CreateTender(request.UserId, request.Tender);
        }
    }
}
