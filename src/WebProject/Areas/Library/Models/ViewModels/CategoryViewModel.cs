using WebProject.Models.Account;

namespace WebProject.Areas.Library.Models.ViewModels
{
    public class CategoryViewModel
    {
        public Library Library { get; set; }
        public Category Category { get; set; }
        public Category SubCategory { get; set; }
        public User User { get; set; }
        public IEnumerable<Article> Articles { get; set; }
        public List<Tag> Tags { get; set; }
        public List<ArticleTag> ArticleTags { get; set; }
    }
}
