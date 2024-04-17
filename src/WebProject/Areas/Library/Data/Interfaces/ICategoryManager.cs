using Microsoft.AspNetCore.Mvc;
using WebProject.Areas.Library.Models;

namespace WebProject.Areas.Library.Data.Interfaces
{
    public interface ICategoryManager
    {
        Task<ActionResult<IEnumerable<Category>>> GetCategoryListByLibraryAsync(string libararyUrl, string userId);
        Task<ActionResult<bool>> CreateCategoryAsync(Category category);
        Task<ActionResult<bool>> UpdateCategoryAsync(Category category);
        List<ArticleCategoriesSort> GetCategoriesSort(int articleId, int libraryId);
        void UpdateArticleCategories(List<ArticleCategoriesSort> categoriesIdsForUpdate, int articleId, int libraryId);
        Task<bool> CheckIsUrlExistAsync(string url, int libraryId, int categoryId);
    }
}
