using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityContentSubmissionPage.Models
{
    public class SubmissionModel
    {
        [DisplayName("Url to Content")]
        public string Url { get; set; }
    }
}
