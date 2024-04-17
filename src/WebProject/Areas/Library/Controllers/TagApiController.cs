using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebProject.Areas.Library.Data.Interfaces;

namespace WebProject.Areas.Library.Controllers
{
    [Area("Library")]
    [ApiController]
    public class TagApiController : ControllerBase
    {
        private readonly ITag _tagRepository;

        public TagApiController(ITag tagRepository)
        {
            _tagRepository = tagRepository;
        }

        [Route("/Library/TagApi/GetTagsByArticleAndUserId")]
        public async Task<JsonResult> GetTagsByArticleAndLibraryIds(int articleId, string userId)
        {
            var result = await _tagRepository.GetTagsSortAsync(articleId, userId);

            JsonResult jsonResult = new(result)
            {
                ContentType = "application/json"
            };
            return jsonResult;
        }
    }
}
