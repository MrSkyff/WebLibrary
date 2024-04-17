using WebProject.Models.Account;
using WebProject.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace WebProject.Data.Repository
{
    public class UserSharedRepository : IUserShared
    {
        private readonly ProjectDbContext _projectDbContext;

        public UserSharedRepository(ProjectDbContext projectDbContext)
        {
            _projectDbContext = projectDbContext;
        }

        public User GetUserByUserName(string userName) => _projectDbContext.Users.FirstOrDefault( x => x.UserName == userName)!;
        
        public async Task<bool> IsUserNameExistAsync(string userName)
        {
            var result = await _projectDbContext.Users.AnyAsync(x => x.UserName == userName);
            return result;
        }

        public async Task<bool> IsEmailExistAsync(string email)
        {
            var result = await _projectDbContext.Users.AnyAsync(x => x.Email == email);
            return result;
        }

        public User GetUserByUserId(string userId)
        {
            var result = _projectDbContext.Users.FirstOrDefault(u => u.Id == userId);
            return result;
        }
    }
}
