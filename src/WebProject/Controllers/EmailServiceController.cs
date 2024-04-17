using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Net;
using System.Threading.Tasks;

namespace WebProject.Controllers
{
    public class EmailServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
