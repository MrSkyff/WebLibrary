namespace WebProject.Data.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string sendTo, string subjectInput, string messageInput);
    }
}
