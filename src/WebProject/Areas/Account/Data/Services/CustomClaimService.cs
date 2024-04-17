using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using WebProject.Models.Account;

namespace WebProject.Areas.Account.Data.Services
{
    public class CustomClaimService : UserClaimsPrincipalFactory<User>
    {
        private readonly UserManager<User> _userManager;

        public CustomClaimService(
            UserManager<User> userManager,
            IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
            _userManager = userManager;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            identity.AddClaim(new Claim("UserId", user.Id ));
           
            foreach (var role in roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            return identity;
        }
    }
}
