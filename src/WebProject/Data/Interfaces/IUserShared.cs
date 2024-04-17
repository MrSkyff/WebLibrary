using WebProject.Models.Account;

namespace WebProject.Data.Interfaces
{
    public interface IUserShared
    {
        User GetUserByUserName(string userName);
        User GetUserByUserId(string userId);
        Task<bool> IsUserNameExistAsync(string userName);
        Task<bool> IsEmailExistAsync(string email);
    }
}
