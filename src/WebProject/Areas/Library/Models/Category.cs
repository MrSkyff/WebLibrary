using System.ComponentModel.DataAnnotations;

namespace WebProject.Areas.Library.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; }
        public int LibraryId { get; set; }
        public string UserId { get; set; }
        public int ParentId { get; set; } = default;

        public string Name { get; set; }     
        public string? Description { get; set; }
        //public Library Library { get; set; } multi cascad error
        public ICollection<ArticleCategory> ArticleCategories { get; set; }
    }
}
