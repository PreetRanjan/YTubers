using Microsoft.AspNetCore.Mvc;

namespace YTubers.Web.Areas.User.Controllers
{
    [Area("User")]
    public class YuTubeController : Controller
    {
        public IActionResult Index() =>  View();
        
    }
}
