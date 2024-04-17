using Microsoft.EntityFrameworkCore;
using WebProject.Areas.Library.Data.Interfaces;
using WebProject.Areas.Library.Models;
using WebProject.Data;

namespace WebProject.Areas.Library.Data.Repositories
{

    public class ArticleRepository : IArticle
    {
        private readonly ProjectDbContext _projectDbContext;

        public ArticleRepository(ProjectDbContext projectDbContext)
        {
            _projectDbContext = projectDbContext;
        }

        public List<Article> GetArticleByLibraryId(int libraryId)
        {
            var result = _projectDbContext.Articles.Where(x => x.LibraryId == libraryId).ToList();
            return result;
        }

        public Article GetArticleById(int articleId)
        {
            var result = _projectDbContext.Articles.FirstOrDefault(x => x.Id == articleId);
            return result;
        }

        public Article GetArticleByUrl(string articleUrl, int libraryId)
        {
            var result = _projectDbContext.Articles.Where(art => art.LibraryId == libraryId).FirstOrDefault(x => x.Url == articleUrl);
            return result;
        }

        public List<ArticlePart> GetArticlePartsByArticleId(int articleId)
        {
            var result = _projectDbContext.ArticleParts.Where(x => x.ArticleId == articleId).ToList();
            return result;
        }

        public List<Article> GetArticlesByCategoryId(int id)
        {
            var result = _projectDbContext.Articles
                .Include(ac => ac.ArticleCategories)
                .Where(ac => ac.ArticleCategories.Any(c => c.CategoryId == id))
                .ToList();
            return result;
        }

        public List<Article> GetArticlesByTitleAndLibraryId(string title, int libraryId)
        {
            return _projectDbContext.Articles.Where(a => a.LibraryId == libraryId && a.Title.Contains(title)).ToList();
        }

        public List<Article> GetArticlesByUserId(string userId)
        {
            return _projectDbContext.Articles.Where(a => a.UserId == userId).ToList();
        }

        public List<Article> GetArticlesByUserIdAndSeachingText(string userId, string searhingText)
        {
            return _projectDbContext.Articles.Where(a => a.UserId == userId && a.Title.Contains(searhingText)).ToList();
        }

        public List<Article> GetArtcilesByTags(List<int> tagIds)
        {
            return _projectDbContext.Articles
                .Include(a => a.ArticleTags)
                .Where(a => a.ArticleTags.Any(at => tagIds.Contains(at.TagId)))
                .ToList();
        }
    }
}
