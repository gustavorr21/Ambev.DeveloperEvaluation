using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Infrastructure.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DefaultContext _context;

        // Construtor para injeção de dependência do contexto do EF Core
        public SaleRepository(DefaultContext context)
        {
            _context = context;
        }

        // Método para criar uma nova venda no banco de dados
        public async Task<SalesEntity> CreateAsync(SalesEntity sale, CancellationToken cancellationToken)
        {
            // Adiciona a venda ao contexto do banco de dados
            await _context.Sales.AddAsync(sale, cancellationToken);

            // Salva as alterações no banco de dados
            await _context.SaveChangesAsync(cancellationToken);

            // Retorna a venda criada (com o ID gerado pelo banco)
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

        // Métodos para SaleItem
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
    }
}
