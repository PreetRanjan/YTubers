using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YTubers.Web.Core.IRepository;
using YTubers.Web.Models;
using YTubers.Web.Utility;

namespace YTubers.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RoleNames.Admin)]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository repo;

        public CategoryController(ICategoryRepository _repo)
        {
            repo = _repo;
        }
        //GET : Get All Catagories
        public async Task<IActionResult> Index()
        {
            return View(await repo.GetCategories());
        }
        //GET:Create
        public IActionResult Create() => View();

        //POST:Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                await repo.AddCategory(category);

                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        //GET:Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = await repo.FindCategory(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        //POST:Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                await repo.UpdateCategory(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        //GET:Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = await repo.FindCategory(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }
                var category = await repo.FindCategory(id);
                if (category == null)
                {
                    return NotFound();
                }
                await repo.DeleteCategory(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                string exce = @"The category you are trying to delete contains
                                references in subcategory and menuitems.
                                If you wish to delete the same remove the all items related to 
                                    this category.";
                
                return RedirectToActionPermanent("HandleException", "Home", new { area = "Customer", msg = exce });
            }
        }
    }
}
