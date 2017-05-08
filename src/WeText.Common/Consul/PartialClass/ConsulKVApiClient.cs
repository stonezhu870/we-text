using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WeText.Common.Consul
{
    /// <summary>
    /// key/value Api客户端
    /// </summary>
    public partial class ConsulRsetApiClient
    {
        protected const string kvApiUrl = "/v1/kv";

        #region 获取键值
        /// <summary>
        /// 获取key/value信息
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public async Task<ConsulApiQueryResult<ConsulKVPair>> KvGetAsync(string key)
        {
            var getKVApiUrl = string.Format("{0}/{1}", kvApiUrl, key);            

            var response = await httpClient.GetAsync(getKVApiUrl).ConfigureAwait(false);
            var queryResult = await response.FormatConsulApiQueryResultAsync<ConsulKVPair[]>().ConfigureAwait(false);

            var result = new ConsulApiQueryResult<ConsulKVPair> { StatusCode = queryResult.StatusCode };
            if (queryResult.ResponseData != null && queryResult.ResponseData.Length > 0)
            {
                result.ResponseData = queryResult.ResponseData[0];
            }
            return result;
        }

        /// <summary>
        /// 获取raw value信息
        /// </summary>
        /// <param name="key">键</param>        
        /// <returns></returns>
        public async Task<ConsulApiQueryResult<string>> KvGetRawAsync(string key)
        {
            var queryApiUriBuilder = new ConsulApiUriBuilder()
                .Path(string.Format("{0}/{1}", kvApiUrl, key))
                .Param("raw", string.Empty);

            var response = await httpClient.GetAsync(queryApiUriBuilder.BuildApiUri()).ConfigureAwait(false);
            return await response.FormatConsulApiQueryResultAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// 获取key/value集合
        /// </summary>
        /// <param name="prefix">键前缀</param>
        /// <returns></returns>
        public async Task<ConsulApiQueryResult<ConsulKVPair[]>> KvGetListAsync(string prefix)
        {
            var queryApiUriBuilder = new ConsulApiUriBuilder()
                .Path(string.Format("{0}/{1}", kvApiUrl, prefix))
                .Param("recurse", string.Empty);

            var response = await httpClient.GetAsync(queryApiUriBuilder.BuildApiUri()).ConfigureAwait(false);

            return await response.FormatConsulApiQueryResultAsync<ConsulKVPair[]>().ConfigureAwait(false);
        }
        #endregion

        #region 获取键
        /// <summary>
        /// 获取key集合
        /// </summary>
        /// <param name="prefix">键前缀</param>
        /// <returns></returns>
        public async Task<ConsulApiQueryResult<string[]>> KvGetKeysAsync(string prefix)
        {
            return await KvGetKeysAsync(prefix, string.Empty).ConfigureAwait(false);
        }

        /// <summary>
        /// 获取key集合
        /// </summary>
        /// <param name="prefix">键前缀</param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public async Task<ConsulApiQueryResult<string[]>> KvGetKeysAsync(string prefix, string separator)
        {
            var queryApiUriBuilder = new ConsulApiUriBuilder()
                .Path(string.Format("{0}/{1}", kvApiUrl, prefix))
                .Param("keys", string.Empty);

            if (!string.IsNullOrEmpty(separator))
            {
                queryApiUriBuilder.Param("separator", separator);
            }

            var response = await httpClient.GetAsync(queryApiUriBuilder.BuildApiUri()).ConfigureAwait(false);

            return await response.FormatConsulApiQueryResultAsync<string[]>().ConfigureAwait(false);
        }
        #endregion

        #region 上传键值
        /// <summary>
        /// 上传key/value信息
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public async Task<ConsulApiWriteResult<bool>> KvPutAsync(string key, string value)
        {
            return await KvPutAsync(key, value, false).ConfigureAwait(false);
        }

        /// <summary>
        /// 上传key/value信息
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="addOnly">是否只添加</param>
        /// <returns></returns>
        public async Task<ConsulApiWriteResult<bool>> KvPutAsync(string key, string value, bool addOnly)
        {
            return await KvPutAsync(key, value, addOnly, 0).ConfigureAwait(false);
        }

        /// <summary>
        /// 上传key/value信息
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="addOnly">是否只添加</param>
        /// <param name="flags">自定义标识</param>
        /// <returns></returns>
        public async Task<ConsulApiWriteResult<bool>> KvPutAsync(string key, string value, bool addOnly, ulong flags)
        {
            var putApiUriBuilder = new ConsulApiUriBuilder().Path(string.Format("{0}/{1}", kvApiUrl, key));
            if (addOnly)
            {
                putApiUriBuilder.Param("cas", "0");
            }
            if (flags > 0)
            {
                putApiUriBuilder.Param("flags", flags.ToString());
            }

            var content = new StringContent(value, Encoding.UTF8);
            var response = await httpClient.PutAsync(putApiUriBuilder.BuildApiUri(), content).ConfigureAwait(false);

            return await response.FormatConsulApiWriteResultAsync<bool>().ConfigureAwait(false);
        }
        #endregion

        #region 删除键值
        /// <summary>
        /// 删除key/value信息
        /// </summary>
        /// <param name="key">键</param>        
        /// <returns></returns>
        public async Task<ConsulApiWriteResult<bool>> KvDeleteAsync(string key)
        {
            var deleteKVApiUrl = string.Format("{0}/{1}", kvApiUrl, key);

            var response = await httpClient.DeleteAsync(deleteKVApiUrl).ConfigureAwait(false);

            return await response.FormatConsulApiWriteResultAsync<bool>().ConfigureAwait(false);
        }

        /// <summary>
        /// 删除key/value集合
        /// </summary>
        /// <param name="prefix">键前缀</param>        
        /// <returns></returns>
        public async Task<ConsulApiWriteResult<bool>> kvDeleteListAsync(string prefix)
        {
            var deleteApiUriBuilder = new ConsulApiUriBuilder()
                .Path(string.Format("{0}/{1}", kvApiUrl, prefix))
                .Param("recurse", string.Empty);

            var response = await httpClient.DeleteAsync(deleteApiUriBuilder.BuildApiUri()).ConfigureAwait(false);

            return await response.FormatConsulApiWriteResultAsync<bool>().ConfigureAwait(false);
        }
        #endregion
    }
}
