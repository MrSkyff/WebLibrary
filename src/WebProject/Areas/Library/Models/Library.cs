using System.ComponentModel.DataAnnotations;
using WebProject.Models.Account;

namespace WebProject.Areas.Library.Models
{
    public class Library
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }    
        public string? Description { get; set; }

    }
}
