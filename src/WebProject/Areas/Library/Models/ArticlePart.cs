namespace WebProject.Areas.Library.Models
{
    public class ArticlePart
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int ParentId { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
