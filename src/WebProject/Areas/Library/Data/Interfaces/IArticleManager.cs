using WebProject.Areas.Library.Models;

namespace WebProject.Areas.Library.Data.Interfaces
{
    public interface IArticleManager
    {
        void CreateArticle(Article article);
        Task<ArticlePart> CreateArticlePartAsync(ArticlePart articlePart);
        Task<bool> DeleteArticlePartAsync(ArticlePart articlePart);
        void UpdateArticle(Article article);
        void UpdateArticleParts(Dictionary<int, ArticlePart> articleParts);
        Task<bool> CheckIsUrlExistAsync(string url, int libraryId);
    }
}
