public class DiscountService
{
    public decimal ApplyDiscount(int quantity, decimal unitPrice)
    {
        if (quantity < 4)
        {
            return 0;
        }

        if (quantity >= 4 && quantity <= 9)
        {
            return 0.10m * (unitPrice * quantity);
        }

        if (quantity >= 10 && quantity <= 20)
        {
            return 0.20m * (unitPrice * quantity);
        }

        throw new InvalidOperationException("Não é possível vender mais de 20 itens idênticos.");
    }
}
