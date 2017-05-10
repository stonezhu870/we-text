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
    internal sealed class HealthChecksEventHandler : 
        IDomainEventHandler<TextCreatedEvent>,
        IDomainEventHandler<TextChangedEvent>
    {
        private readonly ITableDataGateway gateway;

        public HealthChecksEventHandler(ITableDataGateway gateway)
        {
            this.gateway = gateway;
        }

        public async Task Handle(TextCreatedEvent message)
        {
            var textObject = new HealthChecksTableObject
            {
                Id = message.AggregateRootKey.ToString(),
                Title = message.Title,
                Content = message.Content,
                UserId = message.UserId.ToString(),
                DateCreated = message.Timestamp
            };

            await this.gateway.InsertAsync<HealthChecksTableObject>(new[] { textObject });
        }

        public async Task Handle(TextChangedEvent message)
        {
            var id = message.AggregateRootKey.ToString();
            var updateCriteria = new UpdateCriteria<HealthChecksTableObject>();
            if (!string.IsNullOrEmpty(message.Title))
            {
                updateCriteria.Add(x => x.Title, message.Title);
            }
            if (!string.IsNullOrEmpty(message.Content))
            {
                updateCriteria.Add(x => x.Content, message.Content);
            }

            if (updateCriteria.Count == 0)
            {
                return;
            }

            Expression<Func<HealthChecksTableObject, bool>> updateSpecification = x => x.Id == id;
            await gateway.UpdateAsync<HealthChecksTableObject>(updateCriteria, updateSpecification);
        }
    }
}
