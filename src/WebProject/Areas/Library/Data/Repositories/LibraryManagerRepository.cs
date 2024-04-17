using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.Library.Data.Interfaces;
using WebProject.Areas.Library.Models;
using WebProject.Data;

namespace WebProject.Areas.Library.Data.Repositories
{

    public class LibraryManagerRepository : ILibraryManager
    {
        private readonly ProjectDbContext _projectDbContext;

        public LibraryManagerRepository(ProjectDbContext projectDbContext)
        {
            _projectDbContext = projectDbContext;
        }

        public async Task<bool> CheckForSameUrlAsync(string url, string userId)
        {
            bool urlExists = await _projectDbContext.LibraryProjects.Where(lib => lib.UserId == userId).AnyAsync(p => p.Url == url);
            return urlExists;
        }

        public async Task<bool> CreateLibraryProjectAsync(Models.Library libraryProject)
        {
            bool urlStatus = await CheckForSameUrlAsync(libraryProject.Url, libraryProject.UserId);

            if (urlStatus)
            {
                return false;
            }
            else
            {
                await _projectDbContext.LibraryProjects.AddAsync(libraryProject);
                int affectedRows = await _projectDbContext.SaveChangesAsync();

                if (affectedRows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }



        public async Task<IEnumerable<Models.Library>> GetProjectListByUserAsync(string userId)
        {
            var model = await _projectDbContext.LibraryProjects.OrderBy(o => o.Name).Where(project => project.UserId == userId).ToListAsync();

            return model;
        }

        public async Task<bool> UpdateLibraryProjectAsync(Models.Library libraryProject)
        {
            _projectDbContext.LibraryProjects.Update(libraryProject);
            var affectedRows = _projectDbContext.SaveChanges();

            if (affectedRows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> CheckIsUrlExistAsync(string url, string userId, int libraryId)
        {
            var result = await _projectDbContext.LibraryProjects.Where(lib => lib.UserId == userId && lib.Id != libraryId).AnyAsync(p => p.Url == url);
            return result;
        }
    }
}
