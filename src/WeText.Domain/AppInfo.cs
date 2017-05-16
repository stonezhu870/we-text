using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeText.Common;
using WeText.Common.Events;
using WeText.Domain.Events;

namespace WeText.Domain
{
    public class AppInfo : AggregateRoot<Guid>
    {
        public AppInfo()
        {
        }

        public AppInfo(string appkey, string appSecret, string title, string remark,
            string icon, string returnUrl, string createTime)
        {
            
        }

        public string AppKey { get; private set; }

        public string AppSecret { get; private set; }

        public string Title { get; private set; }

        public string Remark { get; private set; }

        public string Icon { get; private set; }

        public string ReturnUrl { get; private set; }

        public bool IsEnable { get; private set; }

        public DateTime CreateTime { get; private set; }
    }
}
