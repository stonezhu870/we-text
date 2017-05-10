using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeText.Common.Consul
{
    public class ConsulService
    {
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="registration"></param>
        public static async void Register(ConsulAgentServiceRegistration registration)
        {
            if (registration.Port == 0)
            {
                registration.Port = Utils.GetPort(registration.Address) ?? 80;
            }

            using (var client = new ConsulRsetApiClient())
            {
                await client.AgentServiceRegisterAsync(registration).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// 注销服务
        /// </summary>
        /// <param name="serviceId"></param>
        public static async void UnRegister(string serviceId)
        {
            using (var client = new ConsulRsetApiClient())
            {
                await client.AgentServiceDeregisterAsync(serviceId).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// 获取所有的服务
        /// </summary>
        /// <returns></returns>
        public static IDictionary<string, ConsulAgentService> GetServices()
        {
            using (var client = new ConsulRsetApiClient())
            {
                var services = client.AgentServicesAsync();

                return services.Result.ResponseData;
            }
        }

        public static ConsulAgentService GetServices(string name)
        {
            var allData = GetServices() ?? new Dictionary<string, ConsulAgentService>();

            if (allData.ContainsKey(name))
            {
                return allData[name];
            }

            return null;
        }

        /// <summary>
        /// 查询服务
        /// </summary>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        public static ConsulServiceEntry[] HealthService(string name)
        {
            using (var client = new ConsulRsetApiClient())
            {
                var services = client.HealthServiceAsync(name);

                return services.Result.ResponseData;
            }
        }

        /// <summary>
        /// 查询服务
        /// </summary>
        /// <param name="name">服务名称</param>
        /// <param name="tag">服务标签</param>
        /// <returns></returns>
        public static ConsulServiceEntry[] HealthServiceAsync(string name, string tag)
        {
            using (var client = new ConsulRsetApiClient())
            {
                var services = client.HealthServiceAsync(name, tag);

                return services.Result.ResponseData;
            }
        }

        /// <summary>
        /// 查询服务
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        /// <param name="tag">服务标签</param>
        /// <param name="passingOnly">只返回健康的服务</param>
        /// <returns></returns>
        public static ConsulServiceEntry[] HealthService(string name, string tag, bool passingOnly)
        {
            using (var client = new ConsulRsetApiClient())
            {
                var services = client.HealthServiceAsync(name, tag, passingOnly);

                return services.Result.ResponseData;
            }
        }
    }
}
