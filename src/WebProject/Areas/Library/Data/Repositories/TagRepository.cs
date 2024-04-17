using Microsoft.EntityFrameworkCore;
using WebProject.Areas.Library.Data.Interfaces;
using WebProject.Areas.Library.Models;
using WebProject.Data;

namespace WebProject.Areas.Library.Data.Repositories
{
    public class TagRepository : ITag
    {
        private readonly ProjectDbContext _dbContext;

        public TagRepository(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ArticleTag> GetArticleTags()
        {
            return _dbContext.ArticleTags.ToList();
        }

        public Tag GetTagByTagName(string tagName)
        {
            return _dbContext.Tags.FirstOrDefault(t => t.Name == tagName);
        }

        public List<Tag> GetTagsByUserId(string userId)
        {
            return _dbContext.Tags.Where(t => t.UserId == userId).ToList();
        }

        public async Task<List<ArticleTagsSort>> GetTagsSortAsync(int articleId, string userId)
        {
            var allTagsByLibraryId = _dbContext.Tags.Where(t => t.UserId == userId).ToList();
            var articleTagsByLibraryId = _dbContext.Tags
                .Where(t => t.UserId == userId)
                .Include(at => at.ArticleTags)
                .Where(at => at.ArticleTags.Any(a => a.ArticleId == articleId))
                .ToList();

            List<ArticleTagsSort> articleTagsSort = new();
            foreach (var tag in articleTagsByLibraryId)
            {
                articleTagsSort.Add(
                    new ArticleTagsSort
                    {
                        Id = tag.Id,
                        Name = tag.Name,
                        UserId = tag.UserId,
                        isAssigned = articleTagsByLibraryId.Contains(tag)
                    });
            }

            return articleTagsSort;
        }
    }
}
