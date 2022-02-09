using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Http;

namespace Proyecto_Intermodular.api
{
    public class DeliiApiClient
    {
        private static readonly HttpClient httpClient = new HttpClient();

        private static string GetToken() => ApplicationState.GetValue<string>("token");

        public static async Task<string> Get(string uri)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetToken());

            HttpResponseMessage response = await httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();
            else
                throw new DeliiApiException(await response.Content.ReadAsStringAsync());
        }


        public static async Task<string> Post(string uri, object data)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetToken());
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            StringContent stringContent = new(JsonSerializer.Serialize(data, GetJsonOptions()));
            stringContent.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = await httpClient.PostAsync(uri, stringContent);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();
            else
                throw new DeliiApiException(await response.Content.ReadAsStringAsync());
        }


        public static async Task<string> Patch(string uri, object data)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetToken());
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            StringContent stringContent = new StringContent(JsonSerializer.Serialize(data, GetJsonOptions()));
            stringContent.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = await httpClient.PatchAsync(uri, stringContent);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();
            else
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                    throw new ItemNotFoundException();
                throw new DeliiApiException(await response.Content.ReadAsStringAsync());
            }
        }


        public static async Task<string> Delete(string uri)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetToken());

            HttpResponseMessage response = await httpClient.DeleteAsync(uri);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();
            else
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                    throw new ItemNotFoundException(await response.Content.ReadAsStringAsync());
                throw new DeliiApiException(await response.Content.ReadAsStringAsync());
            }
        }


        internal static JsonSerializerOptions GetJsonOptions()
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            return options;
        }
    }
}
