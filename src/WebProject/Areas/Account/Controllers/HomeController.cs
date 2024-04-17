using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using System.Security.Principal;
using WebProject.Data.Interfaces;
using WebProject.Models.Account;

namespace WebProject.Areas.Account.Controllers
{
    [Area("Account")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserShared _userSharedRepository;
        private readonly UserManager<User> _userManager;

        public HomeController(IUserShared userSharedRepository, UserManager<User> userManager)
        {
            _userSharedRepository = userSharedRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            var userRoles = await _userManager.GetRolesAsync(user);




            if (user != null)
            {
                return View(user);
            }
                return NotFound();
        }

        
    }
}
