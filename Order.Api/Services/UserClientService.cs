using UserManagement.Apo;
using Grpc.Net.Client;

namespace Order.Api.Services
{
    public class UserClientService
    {
        private readonly UserGrpc.UserGrpcClient _client;

        public UserClientService(IConfiguration configuration)
        {
            var userUrl = configuration["GrpcSettings:UserUrl"] ?? "http://user-management-api:7780";
            var channel = GrpcChannel.ForAddress(userUrl);
            _client = new UserGrpc.UserGrpcClient(channel);
        }

        public async Task<UserResponse> GetUserDetailsAsync(Guid userId)
        {
            try
            {
                var request = new UserRequest { UserId = userId.ToString() };
                return await _client.GetUserAsync(request);
            }
            catch (Exception)
            {
                return new UserResponse { Exists = false };
            }
        }
    }
}
