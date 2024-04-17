using WebProject.Areas.Library.Models;

namespace WebProject.Areas.Library.Data.Interfaces
{
    public interface ILibraryManager
    {
        Task<IEnumerable<Models.Library>> GetProjectListByUserAsync(string userId);
        Task<bool> CreateLibraryProjectAsync(Models.Library libraryProject);
        Task<bool> UpdateLibraryProjectAsync(Models.Library libraryProject);
        Task<bool> CheckForSameUrlAsync(string url, string userId);
        Task<bool> CheckIsUrlExistAsync(string url, string userId, int libraryId);
    }
}
