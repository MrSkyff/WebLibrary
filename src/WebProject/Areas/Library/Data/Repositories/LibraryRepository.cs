using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebProject.Areas.Library.Data.Interfaces;
using WebProject.Data;

namespace WebProject.Areas.Library.Data.Repositories
{
    public class LibraryRepository : ILibrary
    {
        private readonly ProjectDbContext _projectDbContext;

        public LibraryRepository(ProjectDbContext projectDbContext)
        {
            _projectDbContext = projectDbContext;
        }

        public Models.Library GetLibraryByUrl(string url, string userId)
        {
            var result = _projectDbContext.LibraryProjects.Where(lib => lib.UserId == userId).Single(x => x.Url == url);
            return result;
        }
        public Models.Library GetLibraryById(int id)
        {
            var result = _projectDbContext.LibraryProjects.Single(x=>x.Id == id);
            return result; 
        }

        public List<Models.Library> GetLibraryListByUser(string userId)
        {
            return _projectDbContext.LibraryProjects.Where(l => l.UserId == userId).ToList();
        }
    }
}
