using FluentValidation;

public class DeleteSaleRequestValidator : AbstractValidator<int>
{
    public DeleteSaleRequestValidator()
    {
        RuleFor(x => x).GreaterThan(0);
    }
}
