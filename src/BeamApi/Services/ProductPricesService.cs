using System.Collections.Concurrent;
using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class ProductPricesService
{
    private readonly ConcurrentDictionary<string, decimal> _productPrices = new();
    private readonly ConcurrentDictionary<string, decimal> _priceCache = new();
    private readonly ConcurrentBag<PriceHistoryEntry> _priceHistory = new();

    public ApiResponse<object> UpdatePrice(ProductPriceUpdateRequest request)
    {
        var productUpdated = UpdateProductRecord(request.ProductId, request.NewPrice);
        var priceHistoryCreated = AppendPriceHistory(request.ProductId, request.NewPrice);
        var cacheInvalidated = InvalidatePriceCache(request.ProductId);

        return ApiResponse<object>.Success(
            new
            {
                productId = request.ProductId,
                price = request.NewPrice,
                productUpdated,
                priceHistoryCreated,
                cacheInvalidated
            },
            "Product price updated");
    }

    private bool UpdateProductRecord(string productId, decimal newPrice)
    {
        _productPrices[productId] = newPrice;
        return true;
    }

    private bool AppendPriceHistory(string productId, decimal newPrice)
    {
        _priceHistory.Add(new PriceHistoryEntry
        {
            ProductId = productId,
            Price = newPrice,
            RecordedAt = DateTimeOffset.UtcNow
        });
        return true;
    }

    private bool InvalidatePriceCache(string productId)
    {
        _priceCache.TryRemove(productId, out _);
        return true;
    }

    private class PriceHistoryEntry
    {
        public string ProductId { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTimeOffset RecordedAt { get; set; }
    }
}
