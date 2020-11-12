using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityContentSubmissionPage.Business.Logic;
using CommunityContentSubmissionPage.Business.Model;
using CommunityContentSubmissionPage.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommunityContentSubmissionPage.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmitController : ControllerBase
    {
        private readonly ISubmissionSaver _submissionSaver;

        public SubmitController(ISubmissionSaver submissionSaver)
        {
            _submissionSaver = submissionSaver;
        }

        [HttpPost]
        public async Task<IActionResult> Post(SubmissionModel submissionModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var submission = new SubmissionEntry()
            {
                Url = submissionModel.Url,
                Type = submissionModel.Type,
                Email = submissionModel.Email,
                Description = submissionModel.Description
            };

            await _submissionSaver.Save(submission);

            return Ok();
        }
    }
}
