using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using WebProject.Data.Interfaces;

namespace WebProject.Areas.Account.Controllers
{
    [Area("Account")]
    public class IdentityApiController : ControllerBase
    {
        private readonly IUserShared _userSharedRepository;

        public IdentityApiController(IUserShared userSharedRepository)
        {
            _userSharedRepository = userSharedRepository;
        }

        public async Task<JsonResult> IsUserNameExist(string userName)
        {
            var result = await _userSharedRepository.IsUserNameExistAsync(userName);

            var jsonResult = new JsonResult(new { status = result });
            jsonResult.ContentType = "application/json";
            return jsonResult;
        }
    }
}
