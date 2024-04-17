using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using WebProject.Areas.Account.Data.Interfaces;
using WebProject.Areas.Account.Models;
using WebProject.Data.Interfaces;

namespace WebProject.Areas.Account.Controllers
{
    [Area("Account")]
    public class InviteApiController : ControllerBase
    {
        private readonly IInvite _inviteRepository;
        private readonly IUserShared _userSharedRepository;

        public InviteApiController(IInvite inviteRepository, IUserShared userSharedRepository)
        {
            _inviteRepository = inviteRepository;
            _userSharedRepository = userSharedRepository;
        }

        public IActionResult Index()
        {
            return Ok();
        }

        public async Task<JsonResult> GetInviteList()
        {
            var model = _inviteRepository.GetInviteListAsync();
            var jsonResult = new JsonResult(new { model });
            return jsonResult;
        }

        [HttpPost]
        public async Task<JsonResult> CreateInvite([FromBody] Invite invite)
        {
            var model = await _inviteRepository.CreateInviteAsync(invite);

            var jsonResult = new JsonResult(new { model });

            return jsonResult;
        }

        public async Task<JsonResult> GetInviteByCode(string inviteCode)
        {
            var model = _inviteRepository.GetInvtieByInviteCode(inviteCode);
            var jsonResult = new JsonResult(new { model });
            return jsonResult;
        }

        [HttpPost]
        public async Task<JsonResult> UpdateInvite([FromBody] Invite invite)
        {
            var status = await _inviteRepository.UpdateInviteAsync(invite);
            var jsonResult = new JsonResult(status);
            return jsonResult;
        }

        public IActionResult DeleteInvite()
        {
            return Ok();
        }

        public async Task<JsonResult> isEmailExist(string email)
        {
            var isInviteEmailExistResult = await _inviteRepository.isEmailExistAsync(email);
            var isUserEmailExistResult = await _userSharedRepository.IsEmailExistAsync(email);

            var jsonResult = new JsonResult(new { isInviteEmailExist = isInviteEmailExistResult, isUserEmailExist = isUserEmailExistResult });
            jsonResult.ContentType = "application/json";
            return jsonResult;
        }
    }
}
