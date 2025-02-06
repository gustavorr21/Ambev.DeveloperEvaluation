using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Infrastructure.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DefaultContext _context;

        public SaleRepository(DefaultContext context)
        {
            _context = context;
        }
        
        public async Task<SalesEntity> CreateAsync(SalesEntity sale, CancellationToken cancellationToken)
        {
            await _context.Sales.AddAsync(sale, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return sale;
        }

        public async Task<SalesEntity> GetByIdAsync(Guid saleId, CancellationToken cancellationToken)
        {
            return await _context.Sales
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.Id == saleId, cancellationToken);
        }

        public async Task<List<SalesEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Sales
                .Include(s => s.Items)
                .ToListAsync(cancellationToken);
        }

        public async Task UpdateAsync(SalesEntity sale, CancellationToken cancellationToken)
        {
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(SalesEntity sale, CancellationToken cancellationToken)
        {
            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<SaleItemsEntity> CreateSaleItemAsync(SaleItemsEntity saleItem, CancellationToken cancellationToken)
        {
            await _context.SaleItem.AddAsync(saleItem, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return saleItem;
        }

        public async Task DeleteSaleItemAsync(SaleItemsEntity saleItem, CancellationToken cancellationToken)
        {
            _context.SaleItem.Remove(saleItem);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task CancelSaleAsync(Guid saleId, CancellationToken cancellationToken)
        {
            var sale = await _context.Sales.FindAsync(new object[] { saleId }, cancellationToken);
            if (sale == null) throw new Exception("Venda não encontrada.");

            sale.IsCancelled = true;
            _context.Sales.Update(sale);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateSaleAsync(SalesEntity sale, CancellationToken cancellationToken)
        {
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateSaleItemAsync(SaleItemsEntity saleItem, CancellationToken cancellationToken)
        {
            _context.SaleItem.Update(saleItem);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<SaleItemsEntity> GetSaleItemByIdAsync(Guid saleItemId, CancellationToken cancellationToken)
        {
            return await _context.SaleItem
                .AsNoTracking()
                .FirstOrDefaultAsync(item => item.Id == saleItemId, cancellationToken);
        }
    }
}
