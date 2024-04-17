using Microsoft.AspNetCore.Mvc;
using WebProject.Areas.Library.Data.Interfaces;
using WebProject.Areas.Library.Models.ViewModels;
using WebProject.Data.Interfaces;

namespace WebProject.Areas.Library.Controllers
{
    [Area("Library")]
    public class LibraryController : Controller
    {
        private readonly ICategoryManager _categoryManagerRepository;
        private readonly ILibrary _libraryRepository;
        private readonly IArticle _articleRepository;
        private readonly IUserShared _userSharedRepository;
        private readonly ITag _tagRepository;

        public LibraryController(ICategoryManager categoryManagerRepository, ILibrary libraryRepository, IArticle articleRepository, IUserShared userSharedRepository, ITag tagRepository)
        {
            _categoryManagerRepository = categoryManagerRepository;
            _libraryRepository = libraryRepository;
            _articleRepository = articleRepository;
            _userSharedRepository = userSharedRepository;
            _tagRepository = tagRepository;
        }

        [Route("u/{user}/lib/{library}")]
        public IActionResult Index(string user, string library)
        {
            LibraryViewModel model = new();
            model.User = _userSharedRepository.GetUserByUserName(user);
            model.Library = _libraryRepository.GetLibraryByUrl(library, model.User.Id);
            model.Tags = _tagRepository.GetTagsByUserId(model.User.Id);
            model.ArticleTags = _tagRepository.GetArticleTags();
            
            if (model.User != null && model.Library != null)
            {
                model.Articles = _articleRepository.GetArticleByLibraryId(model.Library.Id).OrderByDescending(x => x.Id);
                return View(model);
            }
            else
            {
                return RedirectToAction("NotFound", "Home", new { area = "" });
            }
            
        }
    }
}
