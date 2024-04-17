using Microsoft.AspNetCore.Mvc;
using WebProject.Areas.Library.Data.Interfaces;
using WebProject.Areas.Library.Models;
using WebProject.Areas.Library.Models.Dto;
using WebProject.Data.Interfaces;

namespace WebProject.Areas.Library.Controllers
{
    [Area("Library")]
    public class SearchController : Controller
    {
        private readonly ITag _tagRespository;
        private readonly IArticle _articleRespository;
        private readonly ILibrary _libraryRepository;
        private readonly IUserShared _userSharedRepository;

        public SearchController(ITag tagRespository, IArticle articleRespository, ILibrary libraryRepository, IUserShared userSharedRepository)
        {
            _tagRespository = tagRespository;
            _articleRespository = articleRespository;
            _libraryRepository = libraryRepository;
            _userSharedRepository = userSharedRepository;
        }

        [Route("u/{userName}/Search/{libraryUrl?}/{tagName?}/{searchfor?}")]
        public IActionResult Index(string userName, string libraryUrl = "", string tagName = "", string searchFor = "")
        {
            ArticleSearchDto articleSearchDto = new();
            articleSearchDto.User = _userSharedRepository.GetUserByUserName(userName);
            articleSearchDto.Tags = _tagRespository.GetTagsByUserId(articleSearchDto.User.Id);
            articleSearchDto.ArticleTags = _tagRespository.GetArticleTags();

            if (libraryUrl.Length > 0) { articleSearchDto.Library = _libraryRepository.GetLibraryByUrl(libraryUrl, articleSearchDto.User.Id); }

            articleSearchDto.Libraries = _libraryRepository.GetLibraryListByUser(articleSearchDto.User.Id);

            if (libraryUrl.Length > 0 && searchFor.Length > 0)
            {
                articleSearchDto.Article = _articleRespository.GetArticlesByTitleAndLibraryId(searchFor, articleSearchDto.Library.Id);
            }
            else if (libraryUrl.Length > 0)
            {
                articleSearchDto.Article = _articleRespository.GetArticleByLibraryId(articleSearchDto.Library.Id);
            }
            else if (tagName.Length > 0)
            {
                var tag = _tagRespository.GetTagByTagName(tagName);
                articleSearchDto.Library = new();
                List<int> tags = new List<int>();
                tags.Add(tag.Id);
                articleSearchDto.Article = _articleRespository.GetArtcilesByTags(tags);
            }
            else if (libraryUrl.Length == 0 && searchFor.Length > 0)
            {
                articleSearchDto.Library = new();
                articleSearchDto.Article = _articleRespository.GetArticlesByUserIdAndSeachingText(articleSearchDto.User.Id, searchFor);
            }else if(libraryUrl.Length == 0 && searchFor.Length == 0)
            {
                articleSearchDto.Library = new();
                articleSearchDto.Article = _articleRespository.GetArticlesByUserId(articleSearchDto.User.Id);
            }

            return View(articleSearchDto);
        }
    }
}
