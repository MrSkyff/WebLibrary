using Microsoft.AspNetCore.Mvc;
using WebProject.Areas.Library.Data.Interfaces;
using WebProject.Areas.Library.Models;

namespace WebProject.Areas.Library.Controllers
{
    [Area("Library")]
    public class CategoryManagerApiController : ControllerBase
    {
        private readonly ICategoryManager _categoryManagerRepository;

        public CategoryManagerApiController(ICategoryManager categoryManagerRepository)
        {
            _categoryManagerRepository = categoryManagerRepository;
        }


        public async Task<ActionResult<IEnumerable<Category>>> GetCategoryListByLibrary(string libraryUrl, string userId)
        {
            var result = await _categoryManagerRepository.GetCategoryListByLibraryAsync(libraryUrl, userId);
            return result;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateCategory([FromBody] Category category) 
        {
            var result = await _categoryManagerRepository.CreateCategoryAsync(category);
            return result;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> UpdateCategory([FromBody] Category category)
        {
            var result = await _categoryManagerRepository.UpdateCategoryAsync(category);
            return result;
        }

        public async Task<JsonResult> CheckIsUrlExist(string url , int libraryId, int categoryId)
        {
            var result = await _categoryManagerRepository.CheckIsUrlExistAsync(url, libraryId, categoryId);

            JsonResult jsonResult = new(new { status = result })
            {
                ContentType = "application/json"
            };
            return jsonResult;
        }
    }
}
