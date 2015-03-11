using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofamilies.ClientApi.Identity
{
  public interface IAuthenticationResult
  {
    ICofamiliesIdentity Identity { get; }
  }

  public class AuthenticationResult: IAuthenticationResult
  {
    public AuthenticationResult(string userName, string authenticationType, bool isAuthenticated)
    {
      Identity = new CofamiliesIdentity(userName, authenticationType, isAuthenticated);
    }

    public ICofamiliesIdentity Identity { get; private set; }
  }
}
