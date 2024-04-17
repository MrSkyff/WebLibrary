using Microsoft.AspNetCore.Mvc;
using WebProject.Areas.Library.Data.Interfaces;
using WebProject.Areas.Library.Data.Repositories;
using WebProject.Areas.Library.Models;
using WebProject.Areas.Library.Models.ViewModels;
using WebProject.Data.Interfaces;

namespace WebProject.Areas.Library.Controllers
{
    [Area("Library")]
    public class ArticleManagerController : Controller
    {
        private readonly IArticleManager _articleManagerRespository;
        private readonly IArticle _articleRespository;
        private readonly ICategoryManager _categoryManagerRepository;
        private readonly ILibrary _libraryRepository;
        private readonly IUserShared _userSharedRepository;

        public ArticleManagerController(IArticleManager articleManagerRespository, IArticle articleRespository, ICategoryManager categoryManagerRepository, ILibrary libraryRepository, IUserShared userSharedRepository)
        {
            _articleManagerRespository = articleManagerRespository;
            _articleRespository = articleRespository;
            _categoryManagerRepository = categoryManagerRepository;
            _libraryRepository = libraryRepository;
            _userSharedRepository = userSharedRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateNewArticle(Article article)
        {
            _articleManagerRespository.CreateArticle(article);
            return RedirectToAction("Edit", "ArticleManager", new { area = "Library", value = article.Id });
        }

        [HttpGet]
        public IActionResult Edit(int value)
        {


            ArticleViewModel model = new();
            model.Article = _articleRespository.GetArticleById(value);
            model.ArticleParts = _articleRespository.GetArticlePartsByArticleId(value).ToDictionary(ap => ap.Id, ap => ap);
            model.Categories = _categoryManagerRepository.GetCategoriesSort(model.Article.Id, model.Article.LibraryId);
            model.User = _userSharedRepository.GetUserByUserId(model.Article.UserId);
            model.Library = _libraryRepository.GetLibraryById(model.Article.LibraryId);

            if (User.Identity.IsAuthenticated && model.User.Id == User.FindFirst("UserId").Value)
            {
                return View(model);
            }
            else
            {
                return LocalRedirect("/AccessDenied");
            }

        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(ArticleViewModel model)
        {
            _articleManagerRespository.UpdateArticle(model.Article);
            _articleManagerRespository.UpdateArticleParts(model.ArticleParts);
            _categoryManagerRepository.UpdateArticleCategories(model.Categories, model.Article.Id, model.Article.LibraryId);
            //return RedirectToAction("Edit", "ArticleManager", new { area = "Library"});
            return LocalRedirect("/u/" + model.User.UserName + "/lib/" + model.Library.Url + "/read/" + model.Article.Url);
        }

        public IActionResult GetMainArticlePart([FromBody] ArticlePart? model)
        {
            string partialViewPath = "Areas/Library/Views/ArticleManager/Partials/_ArticlePart_Main_Edit.cshtml";
            return PartialView(partialViewPath, model);
        }

        public IActionResult GetSubArticlePart([FromBody] ArticlePart? model)
        {
            string partialViewPath = "Areas/Library/Views/ArticleManager/Partials/_ArticlePart_Sub_Edit.cshtml";
            return PartialView(partialViewPath, model);
        }

    }
}
