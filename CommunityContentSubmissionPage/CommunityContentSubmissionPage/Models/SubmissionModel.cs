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

        [DisplayName("Type of Content")]
        public string Type { get; set; }

        [DisplayName("Your Email address")]
        public string Email { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("You need to accept our privacy policy to be able to submit an entry")]
        public bool AcceptPrivacyPolicy { get; set; }
    }
}
