using Microsoft.EntityFrameworkCore;
using WebProject.Areas.Library.Data.Interfaces;
using WebProject.Areas.Library.Models;
using WebProject.Data;

namespace WebProject.Areas.Library.Data.Repositories
{
    public class TagManagerRepository : ITagManager
    {
        private readonly ProjectDbContext _projectDbContext;

        public TagManagerRepository(ProjectDbContext projectDbContext)
        {
            _projectDbContext = projectDbContext;
        }

        public void DeleteArticleTag(string tagName, int articleId, string userId)
        {
            Tag? tagToDelete = _projectDbContext.Tags.FirstOrDefault(t => t.UserId == userId && t.Name == tagName);

            if (tagToDelete != null)
            {
                _projectDbContext.ArticleTags.Remove(new ArticleTag { ArticleId = articleId, TagId = tagToDelete.Id });
                _projectDbContext.SaveChanges();

                var articlesCountByTag = _projectDbContext.ArticleTags.Where(t => t.TagId == tagToDelete.Id).Count();
                if (articlesCountByTag == 0)
                {
                    _projectDbContext.Tags.Remove(tagToDelete);
                    _projectDbContext.SaveChanges();
                }

            }
        }

        public void AddArticleTag(string tagName, int articleId, string userId)
        {

            bool tagIsExist = _projectDbContext.Tags.Where(l => l.UserId == userId).Any(t => t.Name == tagName);
            if (tagIsExist)
            {
                int tagId = _projectDbContext.Tags.FirstOrDefault(t => t.Name == tagName).Id;

                var tagIsAssigned = _projectDbContext.ArticleTags.Any(t => t.TagId == tagId && t.ArticleId == articleId);
                if (!tagIsAssigned)
                {
                    _projectDbContext.Add(new ArticleTag { ArticleId = articleId, TagId = tagId });
                    _projectDbContext.SaveChanges();
                }

            }
            else
            {
                Tag newTag = new Tag { Name = tagName, UserId = userId };
                _projectDbContext.Add(newTag);
                _projectDbContext.SaveChanges();

                int tagId = _projectDbContext.Tags.FirstOrDefault(t => t.Name == tagName).Id;
                _projectDbContext.Add(new ArticleTag { ArticleId = articleId, TagId = tagId });
                _projectDbContext.SaveChanges();
            }

        }

        public Task UpdateArticleTags(List<ArticleTagsSort> tagsIdsForUpdate, int articleId, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
