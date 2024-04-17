using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.Library.Data.Interfaces;
using WebProject.Areas.Library.Models;
using WebProject.Data;

namespace WebProject.Areas.Library.Data.Repositories
{
    public class CategoryManagerRepository : ICategoryManager
    {
        private readonly ProjectDbContext _projectDbContext;

        public CategoryManagerRepository(ProjectDbContext projectDbContext)
        {
            _projectDbContext = projectDbContext;
        }

        public async Task<ActionResult<bool>> CreateCategoryAsync(Category category)
        {
            await _projectDbContext.AddAsync(category);
            var affectedRows = await _projectDbContext.SaveChangesAsync();

            if (affectedRows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<ActionResult<IEnumerable<Category>>> GetCategoryListByLibraryAsync(string libraryUrl, string userId)
        {
            var library = await _projectDbContext.LibraryProjects.Where(lib => lib.UserId == userId).FirstOrDefaultAsync(lib => lib.Url == libraryUrl);

            var result = await _projectDbContext.Categories.Where(x => x.LibraryId == library.Id).ToListAsync();
            return result;
        }

        public List<ArticleCategoriesSort> GetCategoriesSort(int articleId, int libraryId)
        {
            var allCategoriesByLibraryId = _projectDbContext.Categories.Where(c => c.LibraryId == libraryId).ToList();
            var articleCategoriesByLibraryId = _projectDbContext.Categories
                .Where(c => c.LibraryId == libraryId)
                .Include(ac => ac.ArticleCategories)
                .Where(ac => ac.ArticleCategories.Any(a => a.ArticleId == articleId))
                .ToList();

            List<ArticleCategoriesSort> articleCategoriesSort = new();
            foreach (var category in allCategoriesByLibraryId)
            {
                articleCategoriesSort.Add(
                    new ArticleCategoriesSort
                    {
                        Id = category.Id,
                        Name = category.Name,
                        Url = category.Url,
                        LibraryId = category.LibraryId,
                        UserId = category.UserId,
                        ParentId = category.ParentId,
                        Description = category.Description,
                        isAssigned = articleCategoriesByLibraryId.Contains(category)
                    });
            }

            return articleCategoriesSort;
        }

        public void UpdateArticleCategories(List<ArticleCategoriesSort> categoriesIdsForUpdate, int articleId, int libraryId)
        {
            var selectedCategoriesHS = new HashSet<int>(categoriesIdsForUpdate.Where(c => c.isAssigned).Select(c => c.Id));
            var articleCategoriesByLibraryIdHS = new HashSet<int>(
                _projectDbContext.Categories.Where(c => c.LibraryId == libraryId)
                .Include(ac => ac.ArticleCategories)
                .Where(ac => ac.ArticleCategories.Any(a => a.ArticleId == articleId))
                .Select(c => c.Id));

            var categoriesByLibraryId = _projectDbContext.Categories.Where(c => c.LibraryId == libraryId).Select(c => c.Id);
            foreach (var categoryId in categoriesByLibraryId)
            {
                if (selectedCategoriesHS.Contains(categoryId))
                {
                    if (!articleCategoriesByLibraryIdHS.Contains(categoryId))
                        _projectDbContext.Add(new ArticleCategory { ArticleId = articleId, CategoryId = categoryId });
                }
                else
                {
                    if (articleCategoriesByLibraryIdHS.Contains(categoryId))
                        _projectDbContext.Remove(new ArticleCategory { ArticleId = articleId, CategoryId = categoryId });
                }
            }

            try
            {
                _projectDbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<bool> CheckIsUrlExistAsync(string url, int libraryId, int categoryId)
        {
            var result = await _projectDbContext.Categories.Where(cat => cat.LibraryId == libraryId && cat.Id != categoryId).AnyAsync(p => p.Url == url);
            return result;
        }

        public async Task<ActionResult<bool>> UpdateCategoryAsync(Category category)
        {
            _projectDbContext.Update(category);
            var affectedRows = await _projectDbContext.SaveChangesAsync();

            if (affectedRows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
