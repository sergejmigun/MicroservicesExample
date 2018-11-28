using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MicroservicesExample.Core.Common.Extensions;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
namespace MicroservicesExample.Core.Api
{
    public class RestClient : IRestClient
    {
        public async Task<TResponse> Get<TResponse>(string url, object data = null)
        {
            var httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(QueryHelpers.AddQueryString(url, data.ToDictionary()));

            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<TResponse>(responseString);
            }

            throw new InvalidOperationException();
        }

        public async Task Post(string url, object data = null)
        {
            var httpClient = new HttpClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(url, stringContent);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException();
            }
        }

        public async Task<TResponse> Post<TResponse>(string url, object data = null)
        {
            var httpClient = new HttpClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(url, stringContent);

            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<TResponse>(responseString);
            }

            throw new InvalidOperationException();
        }

        public async Task Put(string url, object data = null)
        {
            var httpClient = new HttpClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PutAsync(url, stringContent);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException();
            }
        }

        public async Task<TResponse> Put<TResponse>(string url, object data = null)
        {
            var httpClient = new HttpClient();

            var stringContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PutAsync(url, stringContent);

            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<TResponse>(responseString);
            }

            throw new InvalidOperationException();
        }

        public async Task Delete(string url, object data = null)
        {
            var httpClient = new HttpClient();

            HttpResponseMessage response = await httpClient.DeleteAsync(QueryHelpers.AddQueryString(url, data.ToDictionary()));

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException();
            }
        }

        public async Task<TResponse> Delete<TResponse>(string url, object data = null)
        {
            var httpClient = new HttpClient();

            HttpResponseMessage response = await httpClient.DeleteAsync(QueryHelpers.AddQueryString(url, data.ToDictionary()));

            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<TResponse>(responseString);
            }

            throw new InvalidOperationException();
        }
    }
}
