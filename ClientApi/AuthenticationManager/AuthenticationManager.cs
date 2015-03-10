using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofamilies.ClientApi.AuthenticationManager
{
  public interface IAuthenticationManager
  {
    IAuthenticationResult Authenticate(string userName, string password);
    Task<IAuthenticationResult> AuthenticateAsync(string userName, string password);
  }

  public class AuthenticationManager : IAuthenticationManager
  {
    public IAuthenticationResult Authenticate(string userName, string password)
    {
      return new AuthenticationResult(userName, true);
    }

    public async Task<IAuthenticationResult> AuthenticateAsync(string userName, string password)
    {
      return await Task.Run(() => Authenticate(userName, password));
    }
  }
}
