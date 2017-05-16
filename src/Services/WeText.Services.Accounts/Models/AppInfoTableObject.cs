using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeText.Common.Querying;

namespace WeText.Services.Accounts
{
    [ToTable("appinfo")]
    public class AppInfoTableObject
    {
        [MaxLength(32)]
        [System.ComponentModel.DataAnnotations.Key]
        public string AppKey { get; set; }

        [MaxLength(32)]
        [Required]
        public string AppSecret { get; set; }

        [MaxLength(50)]
        [Required]
        public string Title { get; set; }

        [MaxLength(200)]
        public string Remark { get; set; }

        [MaxLength(1024)]
        public string Icon { get; set; }

        [MaxLength(1024)]
        [Required]
        public string ReturnUrl { get; set; }

        [Required]
        public bool IsEnable { get; set; }

        [Required]
        public DateTime CreateTime { get; set; }
    }
}
