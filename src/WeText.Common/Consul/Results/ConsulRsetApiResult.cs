using System.Net;

namespace WeText.Common.Consul
{
    /// <summary>
    /// Consul RsetApi结果
    /// </summary>
    public abstract class ConsulRsetApiResult
    {        
        public HttpStatusCode StatusCode { get; set; }
    }
}
