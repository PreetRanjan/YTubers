using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YTubers.Web.Data;

namespace YTubers.Web.ViewComponents
{
    public class FeaturedYuTuberViewComponent:ViewComponent
    {
        private readonly ApplicationDbContext db;

        public FeaturedYuTuberViewComponent(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var featuredYuTubers = await db.YouTubers.Include(c=>c.Category).Where(x => x.IsFeatured == true).ToListAsync();
            return View(featuredYuTubers);
        }
    }
}
