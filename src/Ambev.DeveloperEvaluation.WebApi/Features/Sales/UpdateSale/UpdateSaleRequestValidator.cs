using FluentValidation;

public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
{
    public UpdateSaleRequestValidator()
    {
        RuleFor(x => x.SaleNumber).NotEmpty();
        RuleFor(x => x.SaleDate).NotEmpty();
        RuleFor(x => x.Client).NotEmpty();
        RuleFor(x => x.TotalValue).GreaterThan(0);
        RuleFor(x => x.Items).NotEmpty();
    }
}
