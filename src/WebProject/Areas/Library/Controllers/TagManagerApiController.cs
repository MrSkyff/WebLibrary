using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebProject.Areas.Library.Data.Interfaces;
using WebProject.Areas.Library.Data.Repositories;
using WebProject.Areas.Library.Models;
using WebProject.Areas.Library.Models.Dto;

namespace WebProject.Areas.Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagManagerApiController : ControllerBase
    {
        private readonly ITagManager _tagManagerRepository;

        public TagManagerApiController(ITagManager tagManagerRepository)
        {
            _tagManagerRepository = tagManagerRepository;
        }

        [Route("/Library/TagManagerApi/GetTagsByArticleAndLibraryIds")]
        public async Task<bool> GetTagsByArticleAndLibraryIds(Tag tag)
        {
            bool t = false; 
            if (tag != null)
            {
                t = true;
            }
            return t;
        }

        [Route("/Library/TagManagerApi/AddTag")]
        [HttpPost]
        public JsonResult AddTag([FromBody] CommandArticleTagDto articleTag)
        {

            _tagManagerRepository.AddArticleTag(articleTag.Name, articleTag.ArticleId, articleTag.UserId);

            JsonResult jsonResult = new(new { status = true })
            {
                ContentType = "application/json"
            };
            return jsonResult;
        }

        [Route("/Library/TagManagerApi/DeleteTag")]
        public async Task<bool> DeleteTag([FromBody] CommandArticleTagDto articleTag)
        {
            _tagManagerRepository.DeleteArticleTag(articleTag.Name, articleTag.ArticleId, articleTag.UserId);
            return true;
        }
    }
}
