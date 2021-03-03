using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using YTubers.Web.Core.IRepository;
using YTubers.Web.Data;
using YTubers.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace YTubers.Web.Persistance.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext db;
        public CategoryRepository(ApplicationDbContext _db)
        {
            db = _db;
        }
        public async Task AddCategory(Category category)
        {
            await db.Categories.AddAsync(category);
            await db.SaveChangesAsync();
        }

        public async Task DeleteCategory(int? id)
        {
            var category = await FindCategory(id);
            db.Categories.Remove(category);
            await db.SaveChangesAsync();
        }

        public async Task<Category> FindCategory(int? id)
        {
            return await db.Categories.FindAsync(id);
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await db.Categories.ToListAsync();
        }

        public async Task<IEnumerable<SelectListItem>> GetCategoriesAsSelectList()
        {
            return (await GetCategories()).Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
        }

        public async Task UpdateCategory(Category category)
        {
            db.Categories.Update(category);
            await db.SaveChangesAsync();
        }
    }
}
