using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityContentSubmissionPage.Models.API;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommunityContentSubmissionPage.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailableTypesController : ControllerBase
    {
        private List<string> _types = new List<string>()
        {
            "Blog Posts",
            "Books",
            "Presentations",
            "Videos",
            "Podcasts",
            "Examples"
        };

        [HttpGet]
        public AvailableTypesModel Get()
        {
            var model = new AvailableTypesModel()
            {
                Types = _types,
                SelectedType = "Blog Posts"
            };

            return model;
        }
    }
}
