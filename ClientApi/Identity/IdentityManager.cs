using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofamilies.ClientApi.Identity
{
  public interface IIdentityManager
  {
    IAuthenticationResult Authenticate(string userName, string password);
    Task<IAuthenticationResult> AuthenticateAsync(string userName, string password);
  }

  public class IdentityManager : IIdentityManager
  {
    public IAuthenticationResult Authenticate(string userName, string password)
    {
      return new AuthenticationResult(userName, "basic", true);
    }

    public async Task<IAuthenticationResult> AuthenticateAsync(string userName, string password)
    {
      return await Task.Run(() => Authenticate(userName, password));
    }
  }
}
