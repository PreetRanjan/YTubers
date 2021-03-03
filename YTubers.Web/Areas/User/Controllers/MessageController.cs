using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YTubers.Web.Core.IRepository;
using YTubers.Web.Models;
using YTubers.Web.Models.ViewModels;

namespace YTubers.Web.Areas.User.Controllers
{
    [Area("User")]
    public class MessageController : Controller
    {
        private readonly IMessageRepository messageRepo;
        private readonly IMapper mapper;

        public MessageController(IMessageRepository messageRepo, IMapper mapper)
        {
            this.messageRepo = messageRepo;
            this.mapper = mapper;
        }
        [HttpPost("/Message/Send")]
        public async Task<IActionResult> SendMessage(MessageViewModel messageVM)
        {
            try
            {
                var userId = (this.User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.NameIdentifier).Value;
                messageVM.SenderId = userId;
                messageVM.When = DateTime.Now;
                messageVM.SenderName = User.Identity.Name.Split('@')[0].ToCamelCase();
                var vc = new System.ComponentModel.DataAnnotations.ValidationContext(messageVM);
                var results = new List<ValidationResult>();
                var isValid = Validator.TryValidateObject(messageVM, vc, results);
                await messageRepo.CreateMessage(mapper.Map<Message>(messageVM));
                return Ok(messageVM);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return Ok(messageVM);
        }
    }
}
