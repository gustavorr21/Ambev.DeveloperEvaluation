using Ambev.DeveloperEvaluation.Domain.Entities;

public class DiscountService
{
    public void ApplyDiscounts(SalesEntity sale)
    {
        foreach (var item in sale.Items)
        {
            if (item.Quantity < 4)
            {
                item.UnitPrice = item.UnitPrice;
            }
            else if (item.Quantity >= 4 && item.Quantity < 10)
            {
                item.UnitPrice *= 0.90m;
            }
            else if (item.Quantity >= 10 && item.Quantity <= 20)
            {
                item.UnitPrice *= 0.80m;
            }
            else if (item.Quantity > 20)
            {
                item.Quantity = 20;
                item.UnitPrice *= 0.80m;
            }
        }
    }
}
