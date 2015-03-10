using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofamilies.ClientApi.AuthenticationManager
{
  public interface IAuthenticationResult
  {
    bool IsAuthenticated { get; }
    string UserName { get; }
  }

  public class AuthenticationResult : IAuthenticationResult
  {
    public AuthenticationResult(string userName, bool isAuthenticated)
    {
      IsAuthenticated = isAuthenticated;
      UserName = userName;
    }

    public bool IsAuthenticated { get; private set; }
    public string UserName { get; private set; }
  }
}
