using WebProject.Areas.Account.Models;

using Microsoft.EntityFrameworkCore;
using WebProject.Areas.Account.Data.Interfaces;
using WebProject.Areas.Account.Models;
using WebProject.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebProject.Areas.Account.Data.Repositories
{
    public class InviteRepository : IInvite
    {
        private readonly ProjectDbContext _projectDbContext;

        public InviteRepository(ProjectDbContext projectDbContext)
        {
            _projectDbContext = projectDbContext;
        }

        public async Task<bool> isEmailExistAsync(string email)
        {
            var result = await _projectDbContext.Invites.AnyAsync(x => x.Email == email);
            return result;
        }

        public async Task<bool> CreateInviteAsync (Invite model)
        {
            model.InviteCode = Guid.NewGuid().ToString();
            model.UseDate = "Not used";
            _projectDbContext.Invites.AddAsync(model);
            var affectedRows = await _projectDbContext.SaveChangesAsync();

            if (affectedRows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateInviteAsync(Invite model)
        {
            _projectDbContext.Invites.Update(model);
            await _projectDbContext.SaveChangesAsync();
            return true;
        }

        public Invite GetInvtieByInviteCode(string inviteCode)
        {          
            Invite invite = new(); 
            if (inviteCode is not null) 
            { 
                var getInvite = _projectDbContext.Invites.SingleOrDefault(x => x.InviteCode == inviteCode);
                if (getInvite is not null)
                {
                    invite = getInvite;
                }
            };
            return invite;
        }

        public async Task<List<Invite>> GetInviteListAsync()
        {
            var result = await _projectDbContext.Invites.ToListAsync();
            return result;
        }

    }
}
