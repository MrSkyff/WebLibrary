using WebProject.Areas.Account.Models;

namespace WebProject.Areas.Account.Data.Interfaces
{
    public interface IInvite
    {
        Task<bool> isEmailExistAsync(string email);
        Task<bool> CreateInviteAsync(Invite model);
        Task<bool> UpdateInviteAsync(Invite model);
        Invite GetInvtieByInviteCode(string inviteCode);
        Task<List<Invite>> GetInviteListAsync();
    }
}
