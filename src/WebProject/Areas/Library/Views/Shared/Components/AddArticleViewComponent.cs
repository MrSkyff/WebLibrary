using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebProject.Areas.Library.Views.Shared.Components
{
    public class AddArticleViewComponent : ViewComponent
    {
        [Authorize]
        public async Task<IViewComponentResult> InvokeAsync(int libraryId)
        {
            ViewBag.LibraryId = libraryId;
            return View();
        }
    }
}
