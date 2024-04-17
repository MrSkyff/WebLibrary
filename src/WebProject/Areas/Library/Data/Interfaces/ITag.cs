using WebProject.Areas.Library.Models;

namespace WebProject.Areas.Library.Data.Interfaces
{
    public interface ITag
    {
        Task<List<ArticleTagsSort>> GetTagsSortAsync(int articleId, string userId);  
        List<Tag> GetTagsByUserId(string userId);
        List<ArticleTag> GetArticleTags();
        Tag GetTagByTagName(string tagName);
    }
}
