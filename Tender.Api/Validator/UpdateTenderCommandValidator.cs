namespace Tender.Api.Validator
{
    using FluentValidation;

    using Tender.ApplicationService.Command.UpdateTender;

    public class UpdateTenderCommandValidator : AbstractValidator<UpdateTenderCommand>
    {
        public UpdateTenderCommandValidator()
        {
            this.RuleFor(x => x.UserId).NotEmpty();
            this.RuleFor(x => x.Tender.TenderId).GreaterThan(0);
            this.RuleFor(x => x.Tender.TenderName).NotEmpty();
            this.RuleFor(x => x.Tender.ContractNo).NotEmpty();
            this.RuleFor(x => x.Tender.ReleaseDate).NotEmpty();
            this.RuleFor(x => x.Tender.ClosingDate).NotEmpty();
            this.RuleFor(x => x.Tender.Description).NotEmpty();
        }
    }
}
