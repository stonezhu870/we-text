namespace WeText.Common.Consul
{
    /// <summary>
    /// Consul 服务Entry信息
    /// </summary>
    public class ConsulServiceEntry
    {
        public ConsulNode Node { get; set; }
        public ConsulAgentService Service { get; set; }
        public ConsulHealthCheck[] Checks { get; set; }
    }
}
