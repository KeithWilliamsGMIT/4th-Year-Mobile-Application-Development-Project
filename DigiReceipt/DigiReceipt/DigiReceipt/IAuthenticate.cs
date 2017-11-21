using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;

namespace DigiReceipt
{
    public interface IAuthenticate
    {
        Task<bool> Authenticate(MobileServiceAuthenticationProvider provider);
    }
}
