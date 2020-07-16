using System.Threading.Tasks;
using iBestRead.Vaptcha.Models;

namespace iBestRead.Vaptcha
{
    public interface IVaptchaClient
    {
        Task<SecondVerifyResponse> SecondVerifyAsync(string token);

    }
}