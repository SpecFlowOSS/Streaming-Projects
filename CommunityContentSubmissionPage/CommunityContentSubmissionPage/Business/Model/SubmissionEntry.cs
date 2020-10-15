using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityContentSubmissionPage.Business.Model
{
    public class SubmissionEntry
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
    }
}
