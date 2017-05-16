using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeText.Common.Commands;

namespace WeText.Domain.Commands
{
    public class CreateAppInfoCommand : Command
    {
        public string AppKey { get; set; }

        public string AppSecret { get; set; }

        public string Title { get; set; }

        public string Remark { get; set; }

        public string Icon { get; set; }

        public string ReturnUrl { get; set; }

        public bool IsEnable { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
