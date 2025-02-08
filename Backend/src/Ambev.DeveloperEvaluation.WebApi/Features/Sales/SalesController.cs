using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.Application.Sale.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sale.DeleteSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

/// <summary>
/// Controller for managing sales operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SalesController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of SalesController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public SalesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new sale
    /// </summary>
    /// <param name="request">The sale creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResult>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return BadRequest(new ApiResponse
            {
                Success = false,
                Message = "Validation failed"
            });
        }

        var command = _mapper.Map<CreateSaleCommand>(request);

        var response = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateSaleResult>
        {
            Success = true,
            Message = "Sale created successfully",
            Data = _mapper.Map<CreateSaleResult>(response)
        });
    }


    /// <summary>
    /// Retrieves a sale by its ID
    /// </summary>
    /// <param name="id">The unique identifier of the sale</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale details if found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSale([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new GetSaleCommand(id);

        var response = await _mediator.Send(command, cancellationToken);

        if (response == null)
            return NotFound(new ApiResponse
            {
                Success = false,
                Message = "Sale not found"
            });

        return Ok(new ApiResponseWithData<GetSaleResponse>
        {
            Success = true,
            Message = "Sale retrieved successfully",
            Data = response
        });
    }

    /// <summary>
    /// get all vendas
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseWithData<IEnumerable<GetSaleResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var command = new GetAllSalesCommand();

        var response = await _mediator.Send(command, cancellationToken);

        if (response == null || !response.Any())
            return NotFound(new ApiResponse
            {
                Success = false,
                Message = "No sales found"
            });

        return Ok(new ApiResponseWithData<IEnumerable<GetSaleResponse>>
        {
            Success = true,
            Message = "Sales retrieved successfully",
            Data = response
        });
    }


    /// <summary>
    /// Deletes a sale by its ID
    /// </summary>
    /// <param name="id">The unique identifier of the sale to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success response if the sale was deleted</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSale([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteSaleCommand { Id = id };
        var result = await _mediator.Send(command, cancellationToken);

        if (!result)
        {
            return NotFound(new ApiResponse
            {
                Success = false,
                Message = "Sale not found"
            });
        }

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Sale deleted successfully"
        });
    }

    /// <summary>
    /// Cancels a sale by its ID
    /// </summary>
    /// <param name="saleId">The sale ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A confirmation message</returns>
    [HttpPut("{saleId}/cancel/{isCancelled}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CancelSale(Guid saleId, bool isCancelled, CancellationToken cancellationToken)
    {
        var command = new CancelSaleCommand(saleId, isCancelled);
        await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Sale cancelled successfully"
        });
    }

    /// <summary>
    /// Cancels a sale item by its ID
    /// </summary>
    /// <param name="saleItemId">The sale item ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A confirmation message</returns>
    [HttpPut("items/{saleItemId}/cancel/{isCancelled}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CancelSaleItem(Guid saleItemId, bool isCancelled, CancellationToken cancellationToken)
    {
        var command = new CancelSaleItemCommand(saleItemId, isCancelled);
        await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Sale item cancelled successfully"
        });
    }

    /// <summary>
    /// Updates a sale
    /// </summary>
    /// <param name="saleId">The sale ID</param>
    /// <param name="request">The sale update request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A confirmation message</returns>
    [HttpPut("{saleId}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSale(Guid saleId, [FromBody] UpdateSaleRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateSaleCommand(saleId, request);
        await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Sale updated successfully"
        });
    }
}
