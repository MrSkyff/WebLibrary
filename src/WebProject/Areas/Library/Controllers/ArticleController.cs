using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WebProject.Areas.Library.Data.Interfaces;
using WebProject.Areas.Library.Models.ViewModels;
using WebProject.Data.Interfaces;

namespace WebProject.Areas.Library.Controllers
{
    [Area("Library")]
    public class ArticleController : Controller
    {
        private readonly IArticleManager _articleManagerRespository;
        private readonly IArticle _articleRespository;
        private readonly ICategoryManager _categoryManagerRepository;
        private readonly ILibrary _libraryRepository;
        private readonly IUserShared _userSharedRepository;
        private readonly ITag _tagRepositery;

        public ArticleController(IArticleManager articleManagerRespository, IArticle articleRespository, ICategoryManager categoryManagerRepository, ILibrary libraryRepository, IUserShared userSharedRepository, ITag tagRepositery)
        {
            _articleManagerRespository = articleManagerRespository;
            _articleRespository = articleRespository;
            _categoryManagerRepository = categoryManagerRepository;
            _libraryRepository = libraryRepository;
            _userSharedRepository = userSharedRepository;
            _tagRepositery = tagRepositery;
        }

        [Route("u/{userName}/lib/{libraryUrl}/read/{articleUrl}")]
        public IActionResult Read(string userName, string libraryUrl, string articleUrl)
        {
            ArticleViewModel model = new();
            model.User = _userSharedRepository.GetUserByUserName(userName);
            model.Library = _libraryRepository.GetLibraryByUrl(libraryUrl, model.User.Id);
            model.Article = _articleRespository.GetArticleByUrl(articleUrl, model.Library.Id);
            model.ArticleParts = _articleRespository.GetArticlePartsByArticleId(model.Article.Id).ToDictionary(ap => ap.Id, ap => ap);
            model.Categories = _categoryManagerRepository.GetCategoriesSort(model.Article.Id, model.Article.LibraryId);
            model.Tags = _tagRepositery.GetTagsByUserId(model.User.Id);
            model.ArticleTags = _tagRepositery.GetArticleTags();
            return View(model);
        }
    }
}
