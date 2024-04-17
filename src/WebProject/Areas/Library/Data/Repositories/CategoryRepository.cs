using WebProject.Areas.Library.Data.Interfaces;
using WebProject.Areas.Library.Models;
using WebProject.Areas.Library.Models.ViewModels;
using WebProject.Data;

namespace WebProject.Areas.Library.Data.Repositories
{
    public class CategoryRepository : ICategory
    {
        private readonly ProjectDbContext _projectDbContext;

        public CategoryRepository(ProjectDbContext projectDbContext)
        {
            _projectDbContext = projectDbContext;
        }

        public CategoryViewModel GetCategoryArticlesByCategoryUrl(string url)
        {
            var category = _projectDbContext.Categories.FirstOrDefault(x => x.Url == url);
            var articleList = _projectDbContext.Articles.Where(x => x.ArticleCategories.Any(ac => ac.Category.Id == category.Id)).ToList();

            CategoryViewModel model = new CategoryViewModel()
            {
                Category = category,
                Articles = articleList,
            };
            return model;
        }

        public List<Category> GetCategoryListByLibrary(int id)
        {
            var result = _projectDbContext.Categories.Where(x => x.LibraryId == id).ToList();
            return result;
        }

        public Category GetCategoryByUrl(string url, int libraryId)
        {
            var result = _projectDbContext.Categories.Where(cat => cat.LibraryId == libraryId).Single(x => x.Url == url);
            return result;
        }
    }
}
