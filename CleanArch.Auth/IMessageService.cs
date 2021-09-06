using System.Threading.Tasks;

namespace CleanArch.Auth
{
    public interface IMessageService
    {
        Task Send(string email, string subject, string message);
    }
}