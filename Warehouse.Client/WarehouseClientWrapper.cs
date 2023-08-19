using Grpc.Core;
using Grpc.Net.Client;
using Warehouse.Server;

namespace Warehouse.Client
{
    public class WarehouseClientWrapper
    {
        private readonly Server.Warehouse.WarehouseClient _client;

        public WarehouseClientWrapper(string address)
        {
            var channel = GrpcChannel.ForAddress(address);
            _client = new Server.Warehouse.WarehouseClient(channel);
        }

        public async Task<Product> GetProductById(string id) 
        {
            try
            {
                var grpcCall = _client.GetProductByIdAsync(new ProductID { Id = id });
                return await CallGrpcServiceAsync(grpcCall);
            }
            catch (RpcException)
            {
                return null;
            }
            catch (Exception)
            {
                throw; 
            }
        }

        public async Task<Product> GetProductByName(string name)
        {
            try
            {
                var grpcCall = _client.GetProductByNameAsync(new ProductName { Name = name });
                return await CallGrpcServiceAsync(grpcCall);
            }
            catch (RpcException)
            {
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> AddProductAsync(string id, string name, int quantity)
        {
            try
            {
                var grpcCall = _client.AddProductAsync(new Product { Id = id, Name = name, Quantity = quantity });
                var response = await CallGrpcServiceAsync(grpcCall);
                return response.Id;
            }
            catch (RpcException)
            {
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Product> UpdateProductAsync(string id, string name, int quantity)
        {
            try
            {
                var grpcCall = _client.UpdateProductAsync(new Product { Id = id, Name = name, Quantity = quantity });
                var response = await CallGrpcServiceAsync(grpcCall);
                return response;
            }
            catch (RpcException)
            {
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<T> CallGrpcServiceAsync<T>(AsyncUnaryCall<T> grpcCall)
        {
            try
            {
                return await grpcCall.ResponseAsync;
            }
            catch (RpcException ex)
            {
                HandleRpcException(ex);
                throw;
            }
        }

        private void HandleRpcException(RpcException ex)
        {
            switch(ex.StatusCode)
            {
                case StatusCode.NotFound:
                    Console.WriteLine($"Error: {ex.Status.Detail}");
                    break;
                case StatusCode.Unavailable:
                    Console.WriteLine($"The server is unavailable.Please try again later");
                    break;
                default:
                    Console.WriteLine($"An error occurred: {ex.Status.Detail}");
                    break;
            }
        }
    }
}
