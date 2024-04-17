namespace WebProject.Areas.Library.Data.Interfaces
{
    public interface ILibrary
    {
        Models.Library GetLibraryByUrl(string id, string userId);
        Models.Library GetLibraryById(int id);
        List<Models.Library> GetLibraryListByUser(string userId);
    }
}
