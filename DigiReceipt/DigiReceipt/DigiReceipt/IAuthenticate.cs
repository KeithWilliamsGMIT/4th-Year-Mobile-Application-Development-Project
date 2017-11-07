using System.Threading.Tasks;

namespace DigiReceipt
{
    public interface IAuthenticate
    {
        Task<bool> Authenticate();
    }
}
