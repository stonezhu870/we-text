using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WeText.Common;
using WeText.Common.Config;
using WeText.Common.Messaging;
using WeText.Common.Querying;
using WeText.Domain.Commands;
using WeText.Services.Common;

namespace WeText.Services.HealthChecks
{
    [RoutePrefix("api")]
    public class HealthChecksController : MicroserviceApiController<HealthChecksService>
    {
        public HealthChecksController(WeTextConfiguration configuration,
            ICommandSender commandSender,
            IEnumerable<Lazy<ITableDataGateway, NamedMetadata>> tableGatewayRegistration)
            :base(configuration, commandSender, tableGatewayRegistration)
        {

        }

        [HttpGet]
        [Route("consul/heartbeat")]
        public IHttpActionResult GetSystemInformation()
        {
            return Ok();
        }
    }
}
