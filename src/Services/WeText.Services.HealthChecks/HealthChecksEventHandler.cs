using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WeText.Common.Events;
using WeText.Common.Querying;
using WeText.Domain.Events;

namespace WeText.Services.HealthChecks
{
    internal sealed class HealthChecksEventHandler
    {
        private readonly ITableDataGateway gateway;

        public HealthChecksEventHandler(ITableDataGateway gateway)
        {
            this.gateway = gateway;
        }
    }
}
