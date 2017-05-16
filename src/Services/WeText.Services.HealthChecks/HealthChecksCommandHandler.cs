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
    internal sealed class HealthChecksCommandHandler
    {
        private IDomainRepository domainRepository;

        public HealthChecksCommandHandler(IDomainRepository domainRepository)
        {
            this.domainRepository = domainRepository;
        }
    }
}
