using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofamilies.ClientApi.Identity
{
  public interface IAuthenticationResult
  {
    ICofamiliesPrincipal Principal { get; }
    bool Success { get; }
  }

  public class AuthenticationResult: IAuthenticationResult
  {
    public AuthenticationResult(string userName, string authenticationType, bool isAuthenticated)
    {
      Success = isAuthenticated;
      if (Success)
      {
        var identity = new CofamiliesIdentity(userName, authenticationType, isAuthenticated);
        Principal = new CofamiliesPrincipal(identity, "user");       
      }
    }

    public ICofamiliesPrincipal Principal { get; private set; }
    public bool Success { get; private set; }
  }
}
