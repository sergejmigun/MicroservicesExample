using System.Threading.Tasks;

namespace MicroservicesExample.Core.Api
{
    public interface IRestClient
    {
        Task<TResponse> Get<TResponse>(string url, object data = null);

        Task Post(string url, object data = null);

        Task<TResponse> Post<TResponse>(string url, object data = null);

        Task Put(string url, object data = null);

        Task<TResponse> Put<TResponse>(string url, object data = null);

        Task Delete(string url, object data = null);

        Task<TResponse> Delete<TResponse>(string url, object data = null);
    }
}
