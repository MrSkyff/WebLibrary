using WebProject.Areas.Library.Models;

namespace WebProject.Areas.Library.Data.Interfaces
{
    public interface ISearch
    {
        Task<List<Article>> ArticleByTagInLibraryAsync(string tag, int libraryId);
        Task<List<Article>> ArticletByTextInLibraryAsync(string text, int libraryId);
        Task<List<Article>> ArticleByTextAndUserIdAsync(string text, string userId);
    }
}
