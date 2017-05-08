using System.Threading.Tasks;

namespace WeText.Common.Consul
{
    /// <summary>
    /// Health Api客户端
    /// </summary>
    public partial class ConsulRsetApiClient
    {
        protected const string healthServiceApiUrl = "/v1/health/service";
        protected const string healthStateApiUrl = "/v1/health/state";

        #region 查询服务
        /// <summary>
        /// 查询服务
        /// </summary>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        public async Task<ConsulApiQueryResult<ConsulServiceEntry[]>> HealthServiceAsync(string name)
        {
            return await HealthServiceAsync(name, string.Empty).ConfigureAwait(false);
        }

        /// <summary>
        /// 查询服务
        /// </summary>
        /// <param name="name">服务名称</param>
        /// <param name="tag">服务标签</param>
        /// <returns></returns>
        public async Task<ConsulApiQueryResult<ConsulServiceEntry[]>> HealthServiceAsync(string name, string tag)
        {
            return await HealthServiceAsync(name, tag, false).ConfigureAwait(false);
        }

        /// <summary>
        /// 查询服务
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        /// <param name="tag">服务标签</param>
        /// <param name="passingOnly">只返回健康的服务</param>
        /// <returns></returns>
        public async Task<ConsulApiQueryResult<ConsulServiceEntry[]>> HealthServiceAsync(string name, string tag, bool passingOnly)
        {
            var queryHealthServiceApiUrl = string.Format("{0}/{1}", healthServiceApiUrl, name);

            var queryApiUriBuilder = new ConsulApiUriBuilder().Path(queryHealthServiceApiUrl);
            if (!string.IsNullOrEmpty(tag))
            {
                queryApiUriBuilder.Param("tag", tag);
            }
            if (passingOnly)
            {
                queryApiUriBuilder.Param("passing", string.Empty);
            }
            var response = await httpClient.GetAsync(queryApiUriBuilder.BuildApiUri()).ConfigureAwait(false);

            return await response.FormatConsulApiQueryResultAsync<ConsulServiceEntry[]>().ConfigureAwait(false);
        }
        #endregion

        /// <summary>
        /// 查询对应状态的Check
        /// </summary>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        public async Task<ConsulApiQueryResult<ConsulHealthCheck[]>> HealthStateAsync(string state)
        {
            var queryHealthStateApiUrl = string.Format("{0}/{1}", healthStateApiUrl, state);
            var response = await httpClient.GetAsync(queryHealthStateApiUrl).ConfigureAwait(false);

            return await response.FormatConsulApiQueryResultAsync<ConsulHealthCheck[]>().ConfigureAwait(false);
        }
    }
}
