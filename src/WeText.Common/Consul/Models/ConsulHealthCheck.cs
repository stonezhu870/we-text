﻿namespace WeText.Common.Consul
{
    public class ConsulHealthCheck
    {
        public string Node { get; set; }
        public string CheckID { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public string Output { get; set; }
        public string ServiceID { get; set; }
        public string ServiceName { get; set; }
    }
}
