using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YTubers.Web.Models;

namespace YTubers.Web.Core.IRepository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<IEnumerable<SelectListItem>> GetCategoriesAsSelectList();
        Task AddCategory(Category category);
        Task UpdateCategory(Category category);
        Task DeleteCategory(int? id);
        Task<Category> FindCategory(int? id);

    }
}
