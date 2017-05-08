using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeText.Common.Consul
{
    /// <summary>
    /// Agent Api客户端
    /// </summary>
    public partial class ConsulRsetApiClient
    {
        protected const string agentChecksApiUrl = "/v1/agent/checks";
        protected const string agentServicesApiUrl = "/v1/agent/services";
        protected const string agentServiceRegisterApiUrl = "/v1/agent/service/register";        
        protected const string agentServiceDeregisterApiUrl = "/v1/agent/service/deregister";

        /// <summary>
        /// Agent Check集合
        /// </summary>
        /// <returns></returns>
        public async Task<ConsulApiQueryResult<IDictionary<string, ConsulHealthCheck>>> AgentChecksAsync()
        {
            var response = await httpClient.GetAsync(agentChecksApiUrl).ConfigureAwait(false);

            return await response.FormatConsulApiQueryResultAsync<IDictionary<string, ConsulHealthCheck>>().ConfigureAwait(false);
        }

        /// <summary>
        /// Agent服务集合
        /// </summary>
        /// <returns></returns>
        public async Task<ConsulApiQueryResult<IDictionary<string, ConsulAgentService>>> AgentServicesAsync()
        {
            var response = await httpClient.GetAsync(agentServicesApiUrl).ConfigureAwait(false);

            return await response.FormatConsulApiQueryResultAsync<IDictionary<string, ConsulAgentService>>().ConfigureAwait(false);
        }

        /// <summary>
        /// Agent服务注册
        /// </summary>
        /// <param name="agentServiceRegistration">服务注册信息</param>
        /// <returns></returns>
        public async Task<ConsulApiWriteResult> AgentServiceRegisterAsync(ConsulAgentServiceRegistration agentServiceRegistration)
        {            
            var registerContent = new StringContent(JsonConvert.SerializeObject(agentServiceRegistration));
            var response = await httpClient.PutAsync(agentServiceRegisterApiUrl, registerContent).ConfigureAwait(false);

            return response.FormatConsulApiWriteResult();
        }

        /// <summary>
        /// Agent服务注销
        /// </summary>
        /// <param name="serviceId">服务Id</param>
        /// <returns></returns>
        public async Task<ConsulApiWriteResult> AgentServiceDeregisterAsync(string serviceId)
        {
            var deregisterServiceApiUrl = string.Format("{0}/{1}", agentServiceDeregisterApiUrl, serviceId);
            var response = await httpClient.GetAsync(deregisterServiceApiUrl).ConfigureAwait(false);

            return response.FormatConsulApiWriteResult();
        }
    }
}
