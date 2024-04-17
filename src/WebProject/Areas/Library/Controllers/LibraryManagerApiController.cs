using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Scripting;
using NuGet.Protocol;
using WebProject.Areas.Library.Data.Interfaces;
using WebProject.Areas.Library.Models;

namespace WebProject.Areas.Library.Controllers
{
    [Area("Library")]
    public class LibraryManagerApiController : ControllerBase
    {
        private readonly ILibraryManager _projectManagerRespository;

        public LibraryManagerApiController(ILibraryManager projectManagerRespository)
        {
            _projectManagerRespository = projectManagerRespository;
        }

        public async Task<ActionResult<IEnumerable<Models.Library>>> GetProjectListByUser(string value)
        {
            var model = await _projectManagerRespository.GetProjectListByUserAsync(value);
            return model.ToList();
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateProject([FromBody] Models.Library libraryProject)
        {
            var result = await _projectManagerRespository.CreateLibraryProjectAsync(libraryProject);
            return result;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> UpdateProject([FromBody] Models.Library libraryProject)
        {
            var result = await _projectManagerRespository.UpdateLibraryProjectAsync(libraryProject);
            return false;
        }

        public async Task<JsonResult> CheckIsUrlExist(string url, string userId, int libraryId)
        {
            var result = await _projectManagerRespository.CheckIsUrlExistAsync(url, userId, libraryId);

            JsonResult jsonResult = new(new { status = result })
            {
                ContentType = "application/json"
            };
            return jsonResult;
        }


    }
}