using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeText.Common;
using WeText.Common.Commands;
using WeText.Common.Repositories;
using WeText.Domain;
using WeText.Domain.Commands;

namespace WeText.Services.HealthChecks
{
    internal sealed class HealthChecksCommandHandler : 
        ICommandHandler<ChangeTextCommand>, 
        ICommandHandler<CreateTextCommand>
    {
        private IDomainRepository domainRepository;

        public HealthChecksCommandHandler(IDomainRepository domainRepository)
        {
            this.domainRepository = domainRepository;
        }

        public async Task Handle(ChangeTextCommand message)
        {
        }

        public async Task Handle(CreateTextCommand message)
        {
        }
    }
}
