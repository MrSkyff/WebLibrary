using WebProject.Models.Account;

namespace WebProject.Areas.Library.Models.ViewModels
{
    public class ArticleViewModel
    {
        public User User { get; set; }
        public Article Article { get; set; }
        public Library Library { get; set; }
        public Dictionary<int ,ArticlePart> ArticleParts { get; set; } = new Dictionary<int ,ArticlePart>();
        public List<ArticleCategoriesSort> Categories { get; set; } = new List<ArticleCategoriesSort>();
        public List<Tag> Tags { get; set; }
        public List<ArticleTag> ArticleTags { get; set; }
    }
}
