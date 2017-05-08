using System.Threading.Tasks;
using System.Collections.Generic;

namespace WeText.Common.Consul
{
    /// <summary>
    /// Consul Agent Api客户端 接口
    /// </summary>
    public interface IConsulAgentApiClient
    {
        /// <summary>
        /// Agent Check集合
        /// </summary>
        /// <returns></returns>
        Task<ConsulApiQueryResult<IDictionary<string, ConsulHealthCheck>>> AgentChecksAsync();

        /// <summary>
        /// Agent服务集合
        /// </summary>
        /// <returns></returns>
        Task<ConsulApiQueryResult<IDictionary<string, ConsulAgentService>>> AgentServicesAsync();

        /// <summary>
        /// Agent服务注册
        /// </summary>
        /// <param name="agentServiceRegistration">服务注册信息</param>
        /// <returns></returns>
        Task<ConsulApiWriteResult> AgentServiceRegisterAsync(ConsulAgentServiceRegistration agentServiceRegistration);

        /// <summary>
        /// Agent服务注销
        /// </summary>
        /// <param name="serviceId">服务Id</param>
        /// <returns></returns>
        Task<ConsulApiWriteResult> AgentServiceDeregisterAsync(string serviceId);
    }
}
