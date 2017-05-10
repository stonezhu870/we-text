using System;
using System.Collections.Generic;
using WeText.Common.Commands;
using WeText.Common.Events;
using WeText.Common.Messaging;
using WeText.Common.Querying;
using WeText.Common.Repositories;
using WeText.Services.Common;
using WeText.Common.Config;
using Autofac;

namespace WeText.Services.HealthChecks
{
    public sealed class HealthChecksServiceRegister : MicroserviceRegister<HealthChecksService>
    {
        private readonly string tableDataGatewayConnectionString;

        public HealthChecksServiceRegister(WeTextConfiguration configuration) : base(configuration)
        {
            
        }

        public new Func<IComponentContext, ITableDataGateway> TableDataGatewayInitializer =
            x => null;


        protected override IEnumerable<Func<IComponentContext, ICommandHandler>> CommandHandlersInitializer
        {
            get
            {
                yield return x => new HealthChecksCommandHandler(x.Resolve<IDomainRepository>());
            }
        }

        protected override IEnumerable<Func<IComponentContext, IDomainEventHandler>> EventHandlersInitializer
        {
            get
            {
                yield return x => new HealthChecksEventHandler(this.ResolveTableDataGateway(x));
            }
        }

        protected override Func<ICommandConsumer, IEventConsumer, HealthChecksService> ServiceInitializer => (cc, ec) => new HealthChecksService(cc, ec);
    }
}
