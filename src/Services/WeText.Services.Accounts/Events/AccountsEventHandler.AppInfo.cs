﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WeText.Common.Events;
using WeText.Common.Querying;
using WeText.Domain.Events;

namespace WeText.Services.Accounts
{
    internal sealed partial class AccountsEventHandler 
        //:
        //IDomainEventHandler<UserCreatedEvent>,
        //IDomainEventHandler<UserDisplayNameChangedEvent>,
        //IDomainEventHandler<UserEmailChangedEvent>
    {
        //public async Task Handle(UserEmailChangedEvent message)
        //{
        //    var accountId = message.AggregateRootKey.ToString();
        //    var updateCriteria = new UpdateCriteria<AccountTableObject> { { x => x.Email, message.Email } };
        //    Expression<Func<AccountTableObject, bool>> updateSpecification = x => x.Id == accountId;
        //    await gateway.UpdateAsync<AccountTableObject>(updateCriteria, updateSpecification);
        //}

        //public async Task Handle(UserDisplayNameChangedEvent message)
        //{
        //    var accountId = message.AggregateRootKey.ToString();
        //    var updateCriteria = new UpdateCriteria<AccountTableObject> { { x => x.DisplayName, message.DisplayName } };
        //    Expression<Func<AccountTableObject, bool>> updateSpecification = x => x.Id == accountId;
        //    await gateway.UpdateAsync<AccountTableObject>(updateCriteria, updateSpecification);
        //}

        //public async Task Handle(UserCreatedEvent message)
        //{
        //    var userTableObject = new AccountTableObject
        //    {
        //        Id = message.AggregateRootKey.ToString(),
        //        Password = message.Password,
        //        Name = message.Name,
        //        Email = message.Email,
        //        DisplayName = message.DisplayName
        //    };
        //    await this.gateway.InsertAsync<AccountTableObject>(new[] { userTableObject });
        //}
    }
}
