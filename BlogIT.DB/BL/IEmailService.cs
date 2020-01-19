using System.Threading.Tasks;

namespace BlogIT.DB.BL
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
