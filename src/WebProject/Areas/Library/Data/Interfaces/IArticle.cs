using WebProject.Areas.Library.Models;

namespace WebProject.Areas.Library.Data.Interfaces
{
    public interface IArticle
    {
        List<Article> GetArticleByLibraryId(int libraryId);
        public Article GetArticleById(int articleId);
        Article GetArticleByUrl(string articleUrl, int libraryId);
        public List<ArticlePart> GetArticlePartsByArticleId(int articleId);
        List<Article> GetArticlesByCategoryId(int id);

        List<Article> GetArticlesByTitleAndLibraryId(string title, int libraryId);

        List<Article> GetArticlesByUserId(string userId);

        List<Article> GetArticlesByUserIdAndSeachingText(string userId, string searhingText);
        List<Article> GetArtcilesByTags(List<int> tagIds);
    }
}
