using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using WeText.Common.Consul;

namespace WeText.Common.Services
{
    public class ServiceProxy : IDisposable
    {
        private string _targetServiceName;

        public ServiceProxy()
        {
        }

        private static ConsulAgentService GetServiceByName(string name, bool foreLoad = false)
        {
            var services = ConsulService.GetServices();

            var kv = services.FirstOrDefault(n => n.Value.Tags != null && n.Value.Tags.Contains(name));

            return kv.Value;
        }

        private static HttpClient CreateHttpClient(string serviceName)
        {
            HttpClient _client = new HttpClient();

            var consulAgent = GetServiceByName(serviceName);

            if (consulAgent == null)
            {
                throw new Exception("没有对应服务");
            }

            var serviceBaseAddress = $"{consulAgent.Address}:{consulAgent.Port}";

            _client.BaseAddress = new Uri(serviceBaseAddress.EndsWith("/") ? serviceBaseAddress : serviceBaseAddress + "/");
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return _client;
        }

        public void Dispose()
        {
           
        }

        #region GetAsync

        public async System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> GetAsync(string serviceName, string requestUri)
        {
            using (var _client = CreateHttpClient(serviceName))
            {
                var apiUrl = GetApiUrlAndSetHttpClient(serviceName, requestUri);

                return await _client.GetAsync(apiUrl);
            }
        }

        public async System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> GetAsync(string serviceName, string requestUri,
            System.Net.Http.HttpCompletionOption completionOption)
        {
            using (var _client = CreateHttpClient(serviceName))
            {
                var apiUrl = GetApiUrlAndSetHttpClient(serviceName, requestUri);

                return await _client.GetAsync(apiUrl, completionOption);
            }
        }

        public async System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> GetAsync(string serviceName, string requestUri,
            System.Net.Http.HttpCompletionOption completionOption, System.Threading.CancellationToken cancellationToken)
        {
            using (var _client = CreateHttpClient(serviceName))
            {
                var apiUrl = GetApiUrlAndSetHttpClient(serviceName, requestUri);

                return await _client.GetAsync(apiUrl, completionOption, cancellationToken);
            }
        }

        public async System.Threading.Tasks.Task<string> GetStringAsync(string serviceName, string requestUri)
        {
            using (var _client = CreateHttpClient(serviceName))
            {
                var apiUrl = GetApiUrlAndSetHttpClient(serviceName, requestUri);

                return await _client.GetStringAsync(apiUrl);
            }
        }

        #endregion

        #region PostAsync

        public async System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> PostAsync(string serviceName, string requestUri, System.Net.Http.HttpContent content)
        {
            using (var _client = CreateHttpClient(serviceName))
            {
                var apiUrl = GetApiUrlAndSetHttpClient(serviceName, requestUri);

                return await _client.PostAsync(apiUrl, content);
            }
        }

        public async System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> PostAsync(string serviceName, string requestUri, System.Net.Http.HttpContent content, System.Threading.CancellationToken cancellationToken)
        {
            using (var _client = CreateHttpClient(serviceName))
            {
                var apiUrl = GetApiUrlAndSetHttpClient(serviceName, requestUri);

                return await _client.PostAsync(apiUrl, content, cancellationToken);
            }
        }

        public async System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> PostAsJsonAsync<T>(string serviceName, string requestUri, T value)
        {
            using (var _client = CreateHttpClient(serviceName))
            {
                var apiUrl = GetApiUrlAndSetHttpClient(serviceName, requestUri);

                return await _client.PostAsJsonAsync<T>(apiUrl, value);
            }
        }

        public async System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> PostAsJsonAsync<T>(string serviceName, string requestUri, T value, System.Threading.CancellationToken cancellationToken)
        {
            using (var _client = CreateHttpClient(serviceName))
            {
                var apiUrl = GetApiUrlAndSetHttpClient(serviceName, requestUri);

                return await _client.PostAsJsonAsync<T>(apiUrl, value, cancellationToken);
            }
        }

        public async System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> PutAsJsonAsync<T>(
            string serviceName, string requestUri, T value)
        {
            using (var _client = CreateHttpClient(serviceName))
            {
                var apiUrl = GetApiUrlAndSetHttpClient(serviceName, requestUri);

                return await _client.PutAsJsonAsync<T>(apiUrl, value);
            }
        }

        public async System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> PutAsJsonAsync<T>(
            string serviceName, string requestUri, T value,
            System.Threading.CancellationToken cancellationToken)
        {
            using (var _client = CreateHttpClient(serviceName))
            {
                var apiUrl = GetApiUrlAndSetHttpClient(serviceName, requestUri);

                return await _client.PutAsJsonAsync<T>(apiUrl, value, cancellationToken);
            }
        }

        #endregion

        #region Delete

        public async System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> DeleteAsync(string serviceName, string requestUri)
        {
            using (var _client = CreateHttpClient(serviceName))
            {
                var apiUrl = GetApiUrlAndSetHttpClient(serviceName, requestUri);

                return await _client.DeleteAsync(apiUrl);
            }
        }

        public async System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> DeleteAsync(string serviceName, string requestUri,
            System.Threading.CancellationToken cancellationToken)
        {
            using (var _client = CreateHttpClient(serviceName))
            {
                var apiUrl = GetApiUrlAndSetHttpClient(serviceName, requestUri);

                return await _client.DeleteAsync(apiUrl, cancellationToken);
            }
        }

        #endregion

        private string GetApiUrlAndSetHttpClient(string serviceName, string requestUri)
        {
            _targetServiceName = serviceName;

            var apiUrl = $"api/{serviceName}/{requestUri}";

            return apiUrl;
        }
    }
}
