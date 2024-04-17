namespace WebProject.Areas.Library.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public ICollection<ArticleTag> ArticleTags { get; set; }
    }
}
