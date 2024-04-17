using WebProject.Models.Account;

namespace WebProject.Areas.Library.Models.ViewModels
{
    public class LibraryViewModel
    {
        public User User { get; set; }
        public Library Library { get; set; }
        public IEnumerable<Article> Articles { get; set; }
        public List<Tag> Tags { get; set; }
        public List<ArticleTag> ArticleTags { get; set; }
    }
}
