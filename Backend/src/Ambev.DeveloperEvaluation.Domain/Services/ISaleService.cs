using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services
{
    public interface ISaleService
    {
        Task<SalesEntity> CreateAsync(SalesEntity sale, CancellationToken cancellationToken);
        Task<SalesEntity> GetByIdAsync(Guid saleId, CancellationToken cancellationToken);
        Task<List<SalesEntity>> GetAllAsync(CancellationToken cancellationToken);
        Task UpdateAsync(SalesEntity sale, CancellationToken cancellationToken);
        Task DeleteAsync(SalesEntity sale, CancellationToken cancellationToken);
        Task<SaleItemsEntity> CreateSaleItemAsync(SaleItemsEntity saleItem, CancellationToken cancellationToken);
        Task DeleteSaleItemAsync(SaleItemsEntity saleItem, CancellationToken cancellationToken);
        Task CancelSaleAsync(Guid saleId, bool isCancelled, CancellationToken cancellationToken);
        Task CancelSaleItemAsync(Guid saleItemId, CancellationToken cancellationToken);
        Task UpdateSaleAsync(SalesEntity sale, CancellationToken cancellationToken);
        Task UpdateSaleItemAsync(SaleItemsEntity saleItem, CancellationToken cancellationToken);
        Task<SaleItemsEntity> GetSaleItemByIdAsync(Guid saleItemId, CancellationToken cancellationToken);
    }
}
