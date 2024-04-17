using WebProject.Models.Account;

namespace WebProject.Areas.Library.Models.Dto
{
    public class ArticleSearchDto
    {
        public User User { get; set; }
        public List<Article> Article { get; set; }
        public Library Library { get; set; }
        public List<Library> Libraries { get; set; }
        public List<Tag> Tags { get; set; }
        public List<ArticleTag> ArticleTags { get; set; }
    }
}
