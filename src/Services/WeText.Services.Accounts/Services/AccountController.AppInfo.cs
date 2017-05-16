using Autofac.Extras.AttributeMetadata;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Http;
using WeText.Common.Messaging;
using WeText.Common.Querying;
using WeText.Domain.Commands;
using WeText.Common;
using System.Threading.Tasks;
using WeText.Common.Specifications;
using System.Linq.Expressions;
using WeText.Services.Common;
using WeText.Common.Config;

namespace WeText.Services.Accounts
{
    public partial class AccountController
    {
        [Route("appinfos/create")]
        [HttpPost]
        public IHttpActionResult CreateAppInfo([FromBody] dynamic model)
        {
            var createUserCommand = new CreateAppInfoCommand
            {
                AppKey = Guid.NewGuid().ToString(),
                AppSecret = model.AppSecret,
                Icon = model.Icon,
                CreateTime = DateTime.Now,
                Id = Guid.NewGuid(),
                IsEnable = true,
                Remark = model.Remark,
                ReturnUrl = model.ReturnUrl,
                Title = model.Title
            };

            CommandSender.Publish(createUserCommand);

            return Ok(createUserCommand.AppKey);
        }

        [Route("appinfos/update/{id}")]
        [HttpPost]
        public IHttpActionResult UpdateAppInfos(Guid id, [FromBody] dynamic model)
        {
            var email = (string)model.Email;
            var displayName = (string)model.DisplayName;
            var updateUserCommand = new UpdateAppInfoCommand
            {
                AppSecret = model.AppSecret,
                Icon = model.Icon,
                IsEnable = true,
                Remark = model.Remark,
                ReturnUrl = model.ReturnUrl,
                Title = model.Title
            };
            CommandSender.Publish(updateUserCommand);
            return Ok();
        }

        [Route("appinfos/id/{id}")]
        public async Task<IHttpActionResult> GetAppInfoById(string id)
        {
            Expression<Func<AppInfoTableObject, bool>> specification = a => a.AppKey == id;
            return Ok(await this.TableDataGateway.SelectAsync<AppInfoTableObject>(specification));
        }
    }
}
