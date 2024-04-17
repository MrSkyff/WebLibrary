using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Evaluation;
using WebProject.Areas.Library.Models;
using WebProject.Areas.Library.Models.ViewModels;
using WebProject.Data.Interfaces;

namespace WebProject.Areas.Library.Controllers
{
    [Area("Library")]
    public class HomeController : Controller
    {
        private readonly IUserShared _userSharedRepository;

        public HomeController(IUserShared userSharedRepository)
        {
            _userSharedRepository = userSharedRepository;
        }

        [Route("u/{user}/libs")]
        public IActionResult Index(string user)
        {
            HomeViewModel model = new()
            {
                User = _userSharedRepository.GetUserByUserName(user)
            };
            return View(model);
        }

    }
}
