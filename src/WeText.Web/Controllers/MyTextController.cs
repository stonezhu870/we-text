﻿using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using WeText.Common;
using WeText.Common.Services;
using WeText.Web.Models;

namespace WeText.Web.Controllers
{
    [Authorize]
    public class MyTextController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var userId = User.Identity.GetUserId();
            using (var proxy = new ServiceProxy())
            {
                var result = await proxy.GetAsync(ServiceList.Texting, $"user/{userId}");
                result.EnsureSuccessStatusCode();
                var model = JsonConvert.DeserializeObject<IEnumerable<TextViewModel>>(await result.Content.ReadAsStringAsync());
                return View(model);
            }
        }

        public async Task<ActionResult> Text(string id)
        {
            using (var proxy = new ServiceProxy())
            {
                var result = await proxy.GetAsync(ServiceList.Texting, $"{id}");
                result.EnsureSuccessStatusCode();
                var model = JsonConvert.DeserializeObject<IEnumerable<TextViewModel>>(await result.Content.ReadAsStringAsync()).FirstOrDefault();
                return View(model);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateTextViewModel model)
        {
            var userId = User.Identity.GetUserId();
            using (var proxy = new ServiceProxy())
            {
                var _client = new HttpClient();
           
                var result = await proxy.PostAsJsonAsync(ServiceList.Texting, "create", new
                {
                    model.Title,
                    model.Content,
                    UserId = userId
                });
                result.EnsureSuccessStatusCode();
                return RedirectToAction("Info", "Home", new
                {
                    MessageTitle = "Success!",
                    MessageText = "Create text request sent successfully.",
                    ReturnAction = "Index",
                    ReturnController = "MyText"
                });
            }
        }

        public async Task<ActionResult> Edit(string id)
        {
            using (var proxy = new ServiceProxy())
            {
                var result = await proxy.GetAsync(ServiceList.Texting, $"{id}");
                result.EnsureSuccessStatusCode();
                var model = JsonConvert.DeserializeObject<IEnumerable<TextViewModel>>(await result.Content.ReadAsStringAsync()).FirstOrDefault();
                return View(new EditTextViewModel { TextId = id, Title = model.Title, Content = model.Content });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditTextViewModel model)
        {
            using (var proxy = new ServiceProxy())
            {
                var result = await proxy.PostAsJsonAsync(ServiceList.Texting, $"update/{model.TextId}", new
                {
                    model.Title,
                    model.Content
                });
                result.EnsureSuccessStatusCode();
                return RedirectToAction("Info", "Home", new
                {
                    MessageTitle = "Success!",
                    MessageText = "Update text request sent successfully.",
                    ReturnAction = "Index",
                    ReturnController = "MyText"
                });
            }
        }
    }
}