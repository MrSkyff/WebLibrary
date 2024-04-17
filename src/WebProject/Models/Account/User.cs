using Microsoft.AspNetCore.Identity;

namespace WebProject.Models.Account
{
    public class User : IdentityUser
    {
        public string? FullName { get; set; }
    }
}
