using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Blockchain_Programming.Services
{
    public sealed class ModexService
    {
        private Token token;
        public Token Token { get => token; set => token = value; }

        public static async Task<ModexService> Create()
        {
            ModexService modex = new ModexService();
            modex.Token = await ObtainToken();
            return modex;
        }

        public static async Task<Token> ObtainToken()
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://bcdb.modex.tech/oauth/token");

            request.Content = new StringContent("username=bcdb.admin@modex.tech&password=ModexBlockchainDatabase&client_id=0x01&client_secret=0x000001&grant_type=password");
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");
            var response = await httpClient.SendAsync(request);
            var reader = new StreamReader(await response.Content.ReadAsStreamAsync());
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            var rawMessage = reader.ReadToEnd();
            return JsonSerializer.Deserialize<Token>(rawMessage);
        }

        public async Task<EntityResponse> CreateEntity(string json, string name)
        {
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://bcdb.modex.tech/data-node01-api/data/catalog/_JsonSchema/" + name);
            request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {token.access_token}");
            request.Content = new StringContent(json);
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            var response = await httpClient.SendAsync(request);
            var reader = new StreamReader(await response.Content.ReadAsStreamAsync());
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            var rawMessage = reader.ReadToEnd();
            Console.WriteLine(rawMessage);
            EntityResponse entityResponse = JsonSerializer.Deserialize<EntityResponse>(rawMessage);
            return entityResponse;
        }
    }
}
