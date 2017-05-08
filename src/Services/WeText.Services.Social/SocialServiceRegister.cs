using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using WeText.Common;
using WeText.Common.Commands;
using WeText.Common.Events;
using WeText.Common.Messaging;
using WeText.Common.Querying;
using WeText.Common.Repositories;
using WeText.Common.Services;
using WeText.Querying.MySqlClient;
using WeText.Services.Common;
using WeText.Common.Config;

namespace WeText.Services.Social
{
    public sealed class SocialServiceRegister : MicroserviceRegister<SocialService>
    {
        private readonly string tableDataGatewayConnectionString;

        public SocialServiceRegister(WeTextConfiguration configuration) : base(configuration)
        {
            this.tableDataGatewayConnectionString = ThisConfiguration?.Settings?.GetItemByKey("TableDataGatewayConnectionString").Value;
            if (string.IsNullOrEmpty(this.tableDataGatewayConnectionString))
            {
                throw new ServiceRegistrationException("Connection String for TableDataGateway has not been specified.");
            }
        }

        protected override Func<IComponentContext, ITableDataGateway> TableDataGatewayInitializer =>
            x => new MySqlTableDataGateway(this.tableDataGatewayConnectionString);

        protected override IEnumerable<Func<IComponentContext, IDomainEventHandler>> EventHandlersInitializer
        {
            get
            {
                yield return x => new SocialEventHandler(x.Resolve<IDomainRepository>(), this.ResolveTableDataGateway(x), this.ResolveGlobalCommandSender(x));
            }
        }

        protected override IEnumerable<Func<IComponentContext, ICommandHandler>> CommandHandlersInitializer
        {
            get
            {
                yield return x => new SocialCommandHandler(x.Resolve<IDomainRepository>());
            }
        }

        protected override Func<ICommandConsumer, IEventConsumer, SocialService> ServiceInitializer => (cc, ec) => new SocialService(cc, ec);
    }
}
