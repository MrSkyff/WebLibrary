using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebProject.Data.Interfaces;
using WebProject.Data.Repository;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserShared _userSharedRepository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IUserShared userSharedRepository, ILogger<HomeController> logger)
        {
            _userSharedRepository = userSharedRepository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("/NotFound")]
        public IActionResult NotFound(string message)
        {
            return View();
        }

        [Route("/AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [Route("/u/{userName}")]
        public IActionResult UserPage(string userName)
        {
            var model = _userSharedRepository.GetUserByUserName(userName);
            return View(model);
        }
    }
}