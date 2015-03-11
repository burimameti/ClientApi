using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Cofamilies.ClientApi.Identity
{
  public interface ICofamiliesIdentity : IIdentity
  {
  }

  public class CofamiliesIdentity : ICofamiliesIdentity
  {
    public CofamiliesIdentity(string name, string authenticationType,bool isAuthenticated)
    {
      Name = name;
      AuthenticationType = authenticationType;
      IsAuthenticated = isAuthenticated;
    }

    public string Name { get; private set; }
    public string AuthenticationType { get; private set; }
    public bool IsAuthenticated { get; private set; }
  }
}
