using Grpc.Net.Client;
using Order.Api.Protos;

namespace Order.Api.Services
{
    public class ProductClientService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ProductClientService> _logger;

        public ProductClientService(IConfiguration configuration, ILogger<ProductClientService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<(bool isAvailable, string message)> CheckProductStockAsync(Guid productId, int quantity)
        {
            var productApiUrl = _configuration["GrpcSettings:ProductUrl"] ?? "http://product-api:8080";

            _logger.LogInformation("Connecting to gRPC Product Service at {Url}", productApiUrl);

            try
            {
                // Note: In a real production environment, you'd use a pool of channels or GrpcClientFactory
                using var channel = GrpcChannel.ForAddress(productApiUrl);
                var client = new InventoryService.InventoryServiceClient(channel);

                var request = new StockRequest
                {
                    ProductId = productId.ToString(),
                    Quantity = quantity
                };

                var response = await client.CheckStockAsync(request);
                return (response.IsAvailable, response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling gRPC Product Service");
                return (false, $"Error contacting Product Service: {ex.Message}");
            }
        }

        public async Task<ProductResponse?> GetProductDetailsAsync(Guid productId)
        {
            var productApiUrl = _configuration["GrpcSettings:ProductUrl"] ?? "http://product-api:8080";

            _logger.LogInformation("Connecting to gRPC Product Service at {Url}", productApiUrl);
            try
            {
                using var channel = GrpcChannel.ForAddress(productApiUrl);
                var client = new InventoryService.InventoryServiceClient(channel);
                var request = new ProductRequest
                {
                    ProductId = productId.ToString()
                };
                var response = await client.GetProductDetailsAsync(request);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling gRPC Product Service");
            
                return null;
            }
        }
    }
}
