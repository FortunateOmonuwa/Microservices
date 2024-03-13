 using PlatformService.DTOs;
using System.Text;
using System.Text.Json;

namespace PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
        }
        public async Task SendPlatformToCommand(PlatformReadDTO platform)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(platform)
                ,Encoding.UTF8
                , "application/json");
            var response = await httpClient.PostAsync($"{configuration["CommandService"]}", httpContent);
            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("Post request to command service was successful");
            }
            else
            {
                Console.WriteLine("Post request to command service was not successful...Please check and try again");
            }
        }
    }
}
