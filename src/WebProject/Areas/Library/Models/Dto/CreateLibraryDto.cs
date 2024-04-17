using WebProject.Models.Account;

namespace WebProject.Areas.Library.Models.Dto
{
    public class CreateLibraryDto
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
