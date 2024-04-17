using WebProject.Areas.Library.Data.Interfaces;
using WebProject.Areas.Library.Models;

namespace WebProject.Areas.Library.Data.Repositories
{
    public class SearchRepository : ISearch
    {
        public Task<List<Article>> ArticleByTagInLibraryAsync(string tag, int libraryId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Article>> ArticleByTextAndUserIdAsync(string text, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Article>> ArticletByTextInLibraryAsync(string text, int libraryId)
        {
            throw new NotImplementedException();
        }
    }
}
