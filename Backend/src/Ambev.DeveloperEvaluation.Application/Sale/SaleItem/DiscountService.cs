using Ambev.DeveloperEvaluation.Domain.Entities;

public class DiscountService
{
    public void ApplyDiscounts(SalesEntity sale)
    {
        foreach (var item in sale.Items)
        {
            decimal originalPrice = item.UnitPrice;
            item.Discount = 0m;

            if (item.Quantity < 4)
            {
                item.UnitPrice = originalPrice;
            }
            else if (item.Quantity >= 4 && item.Quantity < 10)
            {
                item.Discount = originalPrice * 0.10m;
                item.UnitPrice = originalPrice - item.Discount;
            }
            else if (item.Quantity >= 10 && item.Quantity <= 20)
            {
                item.Discount = originalPrice * 0.20m;
                item.UnitPrice = originalPrice - item.Discount;
            }
            else if (item.Quantity > 20)
            {
                item.Quantity = 20;
                item.Discount = originalPrice * 0.20m;
                item.UnitPrice = originalPrice - item.Discount;
            }
        }
    }
}
