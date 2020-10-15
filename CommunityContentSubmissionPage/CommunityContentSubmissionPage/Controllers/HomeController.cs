﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CommunityContentSubmissionPage.Business.Infrastructure;
using CommunityContentSubmissionPage.Business.Logic;
using CommunityContentSubmissionPage.Business.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CommunityContentSubmissionPage.Models;

namespace CommunityContentSubmissionPage.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISubmissionSaver _submissionSaver;

        public HomeController(ILogger<HomeController> logger, ISubmissionSaver submissionSaver)
        {
            _logger = logger;
            _submissionSaver = submissionSaver;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new SubmissionModel());
        }

        [HttpPost]
        public IActionResult Index(SubmissionModel submissionModel)
        {
            var submission = new SubmissionEntry()
            {
                Url = submissionModel.Url,
                Type = submissionModel.Type
            };

            _submissionSaver.Save(submission);

            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
