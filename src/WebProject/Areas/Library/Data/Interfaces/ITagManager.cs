using WebProject.Areas.Library.Models;

namespace WebProject.Areas.Library.Data.Interfaces
{
    public interface ITagManager
    {
        Task UpdateArticleTags(List<ArticleTagsSort> tagsIdsForUpdate, int articleId, string userId);
        void AddArticleTag(string tagName, int articleId, string userId);
        void DeleteArticleTag(string tagName, int articleId, string userId);
    }
}
