using WebProject.Areas.Library.Models;
using WebProject.Areas.Library.Models.ViewModels;

namespace WebProject.Areas.Library.Data.Interfaces
{
    public interface ICategory
    {
        CategoryViewModel GetCategoryArticlesByCategoryUrl(string url);
        List<Category> GetCategoryListByLibrary(int id);
        Category GetCategoryByUrl(string url, int libraryId);
    }
}
