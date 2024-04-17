using Microsoft.AspNetCore.Mvc;
using WebProject.Areas.Library.Data.Interfaces;
using WebProject.Areas.Library.Data.Repositories;
using WebProject.Areas.Library.Models.ViewModels;
using WebProject.Data.Interfaces;

namespace WebProject.Areas.Library.Controllers
{
    [Area("Library")]
    public class CategoryController : Controller
    {
        private readonly ICategory _categoryRepository;
        private readonly ILibrary _libraryRepository;
        private readonly IUserShared _userSharedRepository;
        private readonly IArticle _articleRespository;
        private readonly ITag _tagRepository;

        public CategoryController(ICategory categoryRepository, ILibrary libraryRepository, IUserShared userSharedRepository, IArticle articleRespository, ITag tagRepository)
        {
            _categoryRepository = categoryRepository;
            _libraryRepository = libraryRepository;
            _userSharedRepository = userSharedRepository;
            _articleRespository = articleRespository;
            _tagRepository = tagRepository;
        }

        public IActionResult Index(string value)
        {
            var result = _categoryRepository.GetCategoryArticlesByCategoryUrl(value);
            result.Library = _libraryRepository.GetLibraryById(result.Category.LibraryId);
            return View(result);
        }

        [Route("u/{user}/lib/{library}/cat/{category}")]
        public IActionResult Category(string user, string library, string category) 
        {
            CategoryViewModel model = new()
            {
                User = _userSharedRepository.GetUserByUserName(user),
            };           
            model.Library = _libraryRepository.GetLibraryByUrl(library, model.User.Id);
            model.Category = _categoryRepository.GetCategoryByUrl(category, model.Library.Id);
            model.Articles = _articleRespository.GetArticlesByCategoryId(model.Category.Id).OrderByDescending(x => x.Id);
            model.Tags = _tagRepository.GetTagsByUserId(model.User.Id);
            model.ArticleTags = _tagRepository.GetArticleTags();

            return View(model);
        }

        [Route("u/{user}/lib/{library}/cat/{category}/sub/{subCategory}")]
        public IActionResult SubCategory(string user, string library, string category, string subCategory)
        {
            CategoryViewModel model = new()
            {
                User = _userSharedRepository.GetUserByUserName(user),
            };
            model.Library = _libraryRepository.GetLibraryByUrl(library, model.User.Id);
            model.Category = _categoryRepository.GetCategoryByUrl(category, model.Library.Id);
            model.SubCategory = _categoryRepository.GetCategoryByUrl(subCategory, model.Library.Id);
            model.Articles = _articleRespository.GetArticlesByCategoryId(model.SubCategory.Id).OrderByDescending(x => x.Id);
            model.Tags = _tagRepository.GetTagsByUserId(model.User.Id);
            model.ArticleTags = _tagRepository.GetArticleTags();
            return View(model);
        }
    }
}
