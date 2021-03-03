using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YTubers.Web.Services;
using System.Text;
using System.Text.Json;
using System.IO;
using YTubers.Web.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using YTubers.Web.Core.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;
using YTubers.Web.Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using Bogus;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace YTubers.Web.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IYouTubeFacade facade;
        private readonly ICategoryRepository repo;
        private readonly IMapper mapper;
        private readonly IYuTuberRepository yuRepo;
        private readonly IReachRepository reachRepo;

        public ProfileController(IYouTubeFacade facade, ICategoryRepository repo,IMapper mapper,IYuTuberRepository yuRepo,IReachRepository reachRepo)
        {
            this.facade = facade;
            this.repo = repo;
            this.mapper = mapper;
            this.yuRepo = yuRepo;
            this.reachRepo = reachRepo;
        }
        public async Task<IActionResult> Index()
        {
            var userId = (this.User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.NameIdentifier).Value;
            var isExist = await yuRepo.UserExists(userId);
            if (isExist)
            {
                var userName = await yuRepo.GetUsername(userId);
                return RedirectToAction(nameof(YourProfile),new { username=userName});
            }
            else
            {
                return View();
            }
        }
        [Route("/YuTuber/{username}")]
        public async Task<IActionResult> YourProfile(string username)
        {
            var yuTuber = await yuRepo.GetYuTuber(username);
            var viewModel = mapper.Map<YuTuberViewModel>(yuTuber);
            return View(viewModel);
        }
        [AllowAnonymous]
        [Route("/YuTuberProfile/{username}")]
        public async Task<IActionResult> PublicProfile(string username)
        {
            var yuTuber = await yuRepo.GetYuTuber(username);
            var viewModel = mapper.Map<YuTuberViewModel2>(yuTuber);
            return View(viewModel);
        }
        public async Task<IActionResult> EditYourProfile(string userid)
        {
            var yuTuber = await yuRepo.GetYuTuberById(userid);
            var viewModel = mapper.Map<YuTuberViewModel>(yuTuber);
            ViewBag.Categories = await repo.GetCategoriesAsSelectList();
            return View(viewModel);
        }
        [HttpPost]
        [ActionName("EditYourProfile")]
        public async Task<IActionResult> EditYourProfilePost(YuTuberViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                var yuTuber = mapper.Map<YuTuber>(model);
                await yuRepo.UpdateYuTuber(yuTuber);
                return RedirectToAction(nameof(YourProfile),new { username=yuTuber.ChannelId});
            }
            ViewBag.Categories = await repo.GetCategoriesAsSelectList();
            return View(model);
        }
        public async Task<IActionResult> YuTubeProfile(string ChannelLink, string SearchBy)
        {
            try
            {
                var channelData = await facade.GetChannelDataAsync(ChannelLink);
                return View(channelData);
            }
            catch (Exception ex)
            {
                return View("ExceptionPage", ex.Message);
            }

        }
        public async Task<IActionResult> SignupAsYuTuber(string link)
        {
            var channelData = await facade.GetChannelDataAsync(link);
            var userId = (this.User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.NameIdentifier).Value;
            var yuTuber = new YuTuberViewModel()
            {
                AppUserId = userId,
                ChannelLink = link,
                ChannelName = channelData.Title,
                SubscribersCount = (ulong)channelData.SubsCount,
                VideoCount = (ulong)channelData.VideoCount,
                ThumbnailUrl = channelData.ThumbnailUrl,
                ChannelId = channelData.ChannelId,
                Country = channelData.Country 
            };
            var categories = from c in await repo.GetCategories() select new SelectListItem { Text = c.Name, Value = c.Id.ToString() };
            ViewBag.Categories = await repo.GetCategoriesAsSelectList();
            return View(yuTuber);
        }
        public async Task<IActionResult> FinishSignup(YuTuberViewModel yuTuberViewModel)
        {
            if (ModelState.IsValid)
            {
                var yuTubers = mapper.Map<YuTuber>(yuTuberViewModel);
                var context = new System.ComponentModel.DataAnnotations.ValidationContext(yuTubers);
                ICollection<ValidationResult> results = new List<ValidationResult>();
                var isValid = Validator.TryValidateObject(yuTubers, context, results);
                yuTubers.Category = null;
                if(isValid)
                {
                    await yuRepo.CreateYuTuber(yuTubers);
                    return RedirectToAction(nameof(YourProfile), new { username = yuTubers.ChannelId });
                }
                else
                {
                    return View("ExceptionPage", string.Join(",",results));
                }
            }
            ViewBag.Categories = await repo.GetCategoriesAsSelectList();
            return View("SignupAsYuTuber",yuTuberViewModel);
        }
        public async Task<IActionResult> MyRequests(int page=1)
        {
            string userId = (this.User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.NameIdentifier).Value;
            var requestList = await reachRepo.GetRequestsById(userId,page);
            await reachRepo.MarkMessagesReceived(userId);
            return View(requestList);
        }
        public async Task<IActionResult> MySentRequests(int page = 1)
        {
            string userId = (this.User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.NameIdentifier).Value;
            var requestList = await reachRepo.GetSentRequestsById(userId, page);
            return View(requestList);
        }
        [HttpPost]
        public async Task<IActionResult> ReachOutFormPost(YuTuberViewModel2 viewModel)
        {
            var requestTesh = new Faker<ReachRequest>()
                .RuleFor(c => c.City, f => f.Address.City())
                .RuleFor(c => c.State, f => f.Address.State())
                .RuleFor(c => c.Email, f => f.Internet.Email())
                .RuleFor(c => c.FullName, f => f.Person.FullName)
                .RuleFor(c => c.AppUserId, f => f.PickRandom<Guid>(Guid.NewGuid()).ToString())
                .RuleFor(c => c.Message, f => f.Lorem.Lines())
                .RuleFor(c => c.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(c => c.YuTuberId, f => f.UniqueIndex)
                .RuleFor(c => c.YuTuberUserId, f => f.PickRandom<Guid>(Guid.NewGuid()).ToString())
                .RuleFor(c => c.RequestStatus, f => f.PickRandom<RequestStatus>());
            var request = requestTesh.Generate();
            var userId = (this.User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.NameIdentifier).Value;
            request.YuTuberId = viewModel.Id;
            request.AppUserId = userId;
            request.YuTuberUserId = viewModel.YuTuberUserId;
            request.RequestStatus = RequestStatus.Sent;
            await reachRepo.SendRequest(request);
            //if (ModelState.IsValid)
            //{
            //    var userId = (this.User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.NameIdentifier).Value;
            //    var request = mapper.Map<ReachRequest>(viewModel.ReachModel);
            //    request.YuTuberId = viewModel.Id;
            //    request.AppUserId = userId;
            //    request.YuTuberUserId = viewModel.YuTuberUserId;
            //    request.RequestStatus = RequestStatus.Sent;
            //    await reachRepo.SendRequest(request);
            //    return RedirectToAction(nameof(SuccessPage), new { requestPr = request });
            //}

            return RedirectToAction(nameof(SuccessPage), new { requestPr = request });
        }
        public IActionResult SuccessPage(ReachRequest requestPr = null)
        {
            var requestTesh = new Faker<ReachRequest>()
                .RuleFor(c => c.City, f => f.Address.City())
                .RuleFor(c => c.Email, f => f.Internet.Email())
                .RuleFor(c => c.FullName, f => f.Person.FullName)
                .RuleFor(c => c.AppUserId, f => f.PickRandom<Guid>(Guid.NewGuid()).ToString())
                .RuleFor(c => c.Id, f => f.UniqueIndex)
                .RuleFor(c => c.Message, f => f.Lorem.Lines())
                .RuleFor(c => c.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(c => c.YuTuberId, f => f.UniqueIndex)
                .RuleFor(c => c.YuTuberUserId, f => f.PickRandom<Guid>(Guid.NewGuid()).ToString())
                .RuleFor(c => c.RequestStatus, f => f.PickRandom<RequestStatus>());
            requestPr = requestTesh.Generate();
            return View("ReachRequestSuccess", requestPr);
        }
        [NonAction]
        public string[] GetKeyFromLink(string link)
        {
            string type = "";
            var url = new Uri(link);
            var key = url.Segments;
            if (link.ToLower().Contains("user"))
            {
                type = "user";
            }
            else if (link.ToLower().Contains("channel"))
            {
                type = "channel";
            }
            return new string[] { type, key[key.Length - 1] };
        }

    }
}
