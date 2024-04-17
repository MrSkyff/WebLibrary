

namespace WebProject.Areas.Library.Models
{
    public class Article
    {
        public int Id { get; set; }

        public int LibraryId { get; set; }
        public string UserId { get; set; }


        public string Url { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public ICollection<ArticleCategory> ArticleCategories { get; set; }
        public ICollection<ArticleTag> ArticleTags { get; set; }

    }

}
