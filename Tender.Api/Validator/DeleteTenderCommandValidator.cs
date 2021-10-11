namespace Tender.Api.Validator
{
    using FluentValidation;

    using Tender.ApplicationService.Command.DeleteTender;

    public class DeleteTenderCommandValidator : AbstractValidator<DeleteTenderCommand>
    {
        public DeleteTenderCommandValidator()
        {
            this.RuleFor(x => x.UserId).NotEmpty();
            this.RuleFor(x => x.TenderId).GreaterThan(0);
        }
    }
}
