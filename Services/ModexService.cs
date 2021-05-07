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
    public static class ModexService
    {
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

        public static async Task<EntityResponse> CreateEntity(string json, string name, string access_token)
        {
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://bcdb.modex.tech/data-node01-api/data/catalog/_JsonSchema/" + name);
            request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {access_token}");
            request.Content = new StringContent(json);
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            var response = await httpClient.SendAsync(request);
            var reader = new StreamReader(await response.Content.ReadAsStreamAsync());
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            var rawMessage = reader.ReadToEnd();
            EntityResponse entityResponse = JsonSerializer.Deserialize<EntityResponse>(rawMessage);
            return entityResponse;
        }

        public static async Task<EntityResponse> UploadRecord(string json, string name, string access_token)
        {
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://bcdb.modex.tech/data-node01-api/data/" + name);
            request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {access_token}");
            request.Content = new StringContent(json);
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            var response = await httpClient.SendAsync(request);
            var reader = new StreamReader(await response.Content.ReadAsStreamAsync());
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            var rawMessage = reader.ReadToEnd();
            return JsonSerializer.Deserialize<EntityResponse>(rawMessage);
        }

        public static async Task<Schema> ViewRecord(string schemaName, string transactionId, string access_token)
        {
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(new HttpMethod("GET"), "https://bcdb.modex.tech/data-node01-api/data/" + schemaName + "/" + transactionId);
            request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {access_token}");
            var response = await httpClient.SendAsync(request);
            var reader = new StreamReader(await response.Content.ReadAsStreamAsync());
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            var rawMessage = reader.ReadToEnd();
            return JsonSerializer.Deserialize<Schema>(rawMessage);
        }
    }
}
