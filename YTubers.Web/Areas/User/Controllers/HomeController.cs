using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YTubers.Web.Core.IRepository;
using YTubers.Web.Models;
using YTubers.Web.Models.ViewModels;
using YTubers.Web.Utility;

namespace YTubers.Web.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMessageRepository msgRepo;

        public HomeController(ILogger<HomeController> logger,UserManager<IdentityUser> _userManager,IMessageRepository msgRepo)
        {
            _logger = logger;
            this._userManager = _userManager;
            this.msgRepo = msgRepo;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [Route("/StartChat/{receiverId}")]
        public async Task<IActionResult> StartChat(string receiverId)
        {
            string userId = (this.User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.NameIdentifier).Value;
            var messageVM = new MessageViewModel
            {
                ReceiverId = receiverId,
                SenderId = userId,
                SenderName = User.Identity.Name,
                Text = string.Empty,
                Messages = await msgRepo.GetMessageThread(userId,receiverId),
                ReceiverName = await msgRepo.GetReceiverName(receiverId)
            };
            
            return View(messageVM);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
