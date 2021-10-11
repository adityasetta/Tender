namespace Tender.Api.Validator
{
    using FluentValidation;

    using Tender.ApplicationService.Command.PostTender;

    public class PostTenderCommandValidator : AbstractValidator<PostTenderCommand>
    {
        public PostTenderCommandValidator()
        {
            this.RuleFor(x => x.UserId).NotEmpty();
            this.RuleFor(x => x.Tender.TenderName).NotEmpty();
            this.RuleFor(x => x.Tender.ContractNo).NotEmpty();
            this.RuleFor(x => x.Tender.ReleaseDate).NotEmpty();
            this.RuleFor(x => x.Tender.ClosingDate).NotEmpty();
            this.RuleFor(x => x.Tender.Description).NotEmpty();
        }
    }
}
