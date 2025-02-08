using Ambev.DeveloperEvaluation.Application.Sale.Events;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using MediatR;
using Microsoft.Extensions.Logging;

public class SaleService : ISaleService
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMediator _mediator;
    private readonly ILogger<SaleService> _logger;
    private readonly DiscountService _discountService;

    public SaleService(
        ISaleRepository saleRepository,
        IMediator mediator,
        ILogger<SaleService> logger,
        DiscountService discountService
        )
    {
        _saleRepository = saleRepository;
        _mediator = mediator;
        _logger = logger;
        _discountService = discountService;

    }

    /// <summary>
    /// Cria uma nova venda.
    /// </summary>
    public async Task<SalesEntity> CreateAsync(SalesEntity sale, CancellationToken cancellationToken)
    {
        _discountService.ApplyDiscounts(sale);
        sale.TotalValue = sale.Items.Sum(item => item.Quantity * item.UnitPrice);
        sale.SaleDate = DateTime.Now;

        var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);

        await _mediator.Publish(new SaleCreatedEvent(createdSale.Id), cancellationToken);

        _logger.LogInformation($"Venda {createdSale.Id} criada com sucesso.");
        return createdSale;
    }

    /// <summary>
    /// Obtém uma venda pelo ID.
    /// </summary>
    public async Task<SalesEntity> GetByIdAsync(Guid saleId, CancellationToken cancellationToken)
    {
        return await _saleRepository.GetByIdAsync(saleId, cancellationToken)
            ?? throw new Exception("Venda não encontrada.");
    }

    /// <summary>
    /// Obtém todas as vendas.
    /// </summary>
    public async Task<List<SalesEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _saleRepository.GetAllAsync(cancellationToken);
    }

    /// <summary>
    /// Atualiza uma venda.
    /// </summary>
    public async Task UpdateAsync(SalesEntity sale, CancellationToken cancellationToken)
    {
        await _saleRepository.UpdateAsync(sale, cancellationToken);

        // Publica evento de venda modificada
        await _mediator.Publish(new SaleModifiedEvent(sale.Id), cancellationToken);

        _logger.LogInformation($"Venda {sale.Id} atualizada.");
    }

    /// <summary>
    /// Deleta uma venda.
    /// </summary>
    public async Task DeleteAsync(SalesEntity sale, CancellationToken cancellationToken)
    {
        await _saleRepository.DeleteAsync(sale, cancellationToken);

        _logger.LogInformation($"Venda {sale.Id} deletada.");
    }

    /// <summary>
    /// Adiciona um item à venda.
    /// </summary>
    public async Task<SaleItemsEntity> CreateSaleItemAsync(SaleItemsEntity saleItem, CancellationToken cancellationToken)
    {
        var createdItem = await _saleRepository.CreateSaleItemAsync(saleItem, cancellationToken);

        _logger.LogInformation($"Item {createdItem.Id} adicionado à venda {saleItem.SaleId}.");
        return createdItem;
    }

    /// <summary>
    /// Remove um item da venda.
    /// </summary>
    public async Task DeleteSaleItemAsync(SaleItemsEntity saleItem, CancellationToken cancellationToken)
    {
        await _saleRepository.DeleteSaleItemAsync(saleItem, cancellationToken);

        await _mediator.Publish(new ItemCancelledEvent(saleItem.SaleId, saleItem.Id), cancellationToken);

        _logger.LogInformation($"Item {saleItem.Id} removido da venda {saleItem.SaleId}.");
    }

    /// <summary>
    /// Cancela uma venda inteira.
    /// </summary>
    public async Task CancelSaleAsync(Guid saleId, bool isActive, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(saleId, cancellationToken);
        if (sale == null) throw new Exception("Venda não encontrada.");

        sale.IsCancelled = isActive;

        foreach (var item in sale.Items)
        {
            item.IsCancelled = isActive;
            await _mediator.Publish(new SaleItemCancelledEvent(item.Id), cancellationToken);
        }

        await _saleRepository.UpdateAsync(sale, cancellationToken);

        await _mediator.Publish(new SaleCancelledEvent(saleId), cancellationToken);

        _logger.LogInformation($"Venda {saleId} e seus itens foram cancelados.");
    }


    /// <summary>
    /// Cancela um item de venda
    /// </summary>
    public async Task CancelSaleItemAsync(Guid saleItemId, CancellationToken cancellationToken)
    {
        var saleItem = await _saleRepository.GetSaleItemByIdAsync(saleItemId, cancellationToken);
        if (saleItem == null) throw new Exception("Item de venda não encontrado.");

        saleItem.IsCancelled = true;

        await _saleRepository.UpdateSaleItemAsync(saleItem, cancellationToken);
    }

    /// <summary>
    /// Atualiza a Venda
    /// </summary>    
    public async Task UpdateSaleAsync(SalesEntity sale, CancellationToken cancellationToken)
    {
        var existingSale = await _saleRepository.GetByIdAsync(sale.Id, cancellationToken);
        if (existingSale == null) throw new Exception("Venda não encontrada.");

        existingSale.SaleDate = sale.SaleDate;
        existingSale.TotalValue = sale.TotalValue;
        existingSale.IsCancelled = sale.IsCancelled;
        existingSale.SaleNumber = sale.SaleNumber;
        existingSale.Client = sale.Client;
        existingSale.Branch = sale.Branch;

        await _saleRepository.UpdateAsync(existingSale, cancellationToken);
    }

    /// <summary>
    /// Atualiza o item da Venda
    /// </summary>    
    public async Task UpdateSaleItemAsync(SaleItemsEntity saleItem, CancellationToken cancellationToken)
    {
        var existingSaleItem = await _saleRepository.GetSaleItemByIdAsync(saleItem.Id, cancellationToken);
        if (existingSaleItem == null) throw new Exception("Item da venda não encontrado.");

        existingSaleItem.Product = saleItem.Product;
        existingSaleItem.Quantity = saleItem.Quantity;
        existingSaleItem.UnitPrice = saleItem.UnitPrice;
        existingSaleItem.IsCancelled = saleItem.IsCancelled;
        existingSaleItem.Discount = saleItem.Discount;

        await _saleRepository.UpdateSaleItemAsync(existingSaleItem, cancellationToken);
    }

    public async Task<SaleItemsEntity> GetSaleItemByIdAsync(Guid saleItemId, CancellationToken cancellationToken)
    {
        var saleItem = await _saleRepository.GetSaleItemByIdAsync(saleItemId, cancellationToken);
        if (saleItem == null) throw new Exception("Item da venda não encontrado.");

        return saleItem;
    }
}
