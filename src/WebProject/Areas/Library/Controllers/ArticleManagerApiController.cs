using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using WebProject.Areas.Library.Data.Interfaces;
using WebProject.Areas.Library.Models;

namespace WebProject.Areas.Library.Controllers
{
    [Area("Library")]
    public class ArticleManagerApiController : ControllerBase
    {

        private readonly IArticleManager _articleManagerRepository;

        public ArticleManagerApiController(IArticleManager articleManagerRepository)
        {
            _articleManagerRepository = articleManagerRepository;
        }

        public async Task<ActionResult<ArticlePart>> CreateArticlePart([FromBody] ArticlePart articlePart)
        {
            var model = await _articleManagerRepository.CreateArticlePartAsync(articlePart);
            return model;
        }

        public async Task<ActionResult<bool>> DeleteArticlePart([FromBody] ArticlePart articlePart)
        {
            bool status = await _articleManagerRepository.DeleteArticlePartAsync(articlePart);
            return status;
        }

        public async Task<JsonResult> CheckIsUrlExist(string url, int libraryId)
        {
            var result = await _articleManagerRepository.CheckIsUrlExistAsync(url, libraryId);

            JsonResult jsonResult = new(new { status = result })
            {
                ContentType = "application/json"
            };
            return jsonResult;
        }
    }
}
