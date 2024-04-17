using Microsoft.EntityFrameworkCore;
using WebProject.Areas.Library.Data.Interfaces;
using WebProject.Areas.Library.Models;
using WebProject.Data;

namespace WebProject.Areas.Library.Data.Repositories
{
    public class ArticleManagerRespository : IArticleManager
    {
        private readonly ProjectDbContext _projectDbContext;

        public ArticleManagerRespository(ProjectDbContext projectDbContext)
        {
            _projectDbContext = projectDbContext;
        }

        public void CreateArticle(Article article)
        {
            //If arcticle description is null assign empty string.
            article.Description ??= "";

            _projectDbContext.Articles.Add(article);
            _projectDbContext.SaveChanges();
        }

        public async Task<ArticlePart> CreateArticlePartAsync(ArticlePart articlePart)
        {
            await _projectDbContext.ArticleParts.AddAsync(articlePart);
            await _projectDbContext.SaveChangesAsync();
            return articlePart;
        }

        public async Task<bool> DeleteArticlePartAsync(ArticlePart articlePart)
        {
            if (articlePart.ParentId == 0)
            {
                int subArticlePartsCount = await _projectDbContext.ArticleParts.Where(x => x.ParentId == articlePart.Id).CountAsync();
                if (subArticlePartsCount > 0)
                {
                    return false;
                }
                else
                {
                    _projectDbContext.ArticleParts.Remove(articlePart);
                    await _projectDbContext.SaveChangesAsync();
                    return true;
                }
            }
            else
            {
                _projectDbContext.ArticleParts.Remove(articlePart);
                await _projectDbContext.SaveChangesAsync();
                return true;
            }
        }

        public void UpdateArticle(Article article)
        {
            _projectDbContext.Articles.Update(article);
            _projectDbContext.SaveChanges();
        }

        public void UpdateArticleParts(Dictionary<int, ArticlePart> articleParts)
        {
            _projectDbContext.ArticleParts.UpdateRange(articleParts.Values);
            _projectDbContext.SaveChanges();
        }

        public async Task<bool> CheckIsUrlExistAsync(string url, int libraryId)
        {
            var result = await _projectDbContext.Articles.Where(art => art.LibraryId == libraryId).AnyAsync(p => p.Url == url);
            return result;
        }

    }
}
