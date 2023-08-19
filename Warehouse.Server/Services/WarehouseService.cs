using Grpc.Core;

namespace Warehouse.Server.Services
{
    public class WarehouseService : Warehouse.WarehouseBase
    {
        private static Dictionary<string, Product> _products = new Dictionary<string, Product>();

        public override Task<Product> GetProductById(ProductID request, ServerCallContext context)
        {
            if(_products.TryGetValue(request.Id, out Product product) && product !=null) 
                return Task.FromResult(product);
            else
            {
                var errorResponse = new ErrorResponse
                {
                    Reason = "Product not found.",
                    Details = { $"No product with ID {request.Id} exists." }
                };

                context.Status = new Status(StatusCode.NotFound, $"{errorResponse.Reason}. Detail: {errorResponse.Details}");
                return Task.FromResult(new Product());
            }
        }

        public override Task<Product> GetProductByName(ProductName request, ServerCallContext context)
        {
            Product matchingProduct = null!;

            foreach (var product in _products.Values)
            {
                matchingProduct = product;
                break;
            }

            if (matchingProduct != null)
                return Task.FromResult(matchingProduct);
            else
            {
                var errorResponse = new ErrorResponse
                {
                    Reason = "Product not found.",
                    Details = { $"No product with name: {request.Name} exists." }
                };

                context.Status = new Status(StatusCode.NotFound, $"{errorResponse.Reason}. Detail: {errorResponse.Details}");
                return Task.FromResult(new Product());
            }
        }

        public override Task<ProductID> AddProduct(Product request, ServerCallContext context)
        {
            _products[request.Id] = request;
            return Task.FromResult(new ProductID { Id = request.Id });
        }

        public override Task<Product> UpdateProduct(Product request, ServerCallContext context)
        {
            if (_products.ContainsKey(request.Id))
            {
                _products[request.Id] = request;
                return Task.FromResult(request);
            }
            else
            {
                var errorResponse = new ErrorResponse
                {
                    Reason = "Product not found.",
                    Details = { $"No product with ID: {request.Id} exists." }
                };

                context.Status = new Status(StatusCode.NotFound, $"{errorResponse.Reason}. Detail: {errorResponse.Details}");
                return Task.FromResult(new Product());
            }
        }
    }
}
