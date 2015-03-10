using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Rob.Core;

namespace Cofamilies.ClientApi.Identity
{
  public interface ICofamiliesPrincipal : IPrincipal
  {
    ICofamiliesIdentity CofamiliesIdentity { get; }
  }

  public class CofamiliesPrincipal : ICofamiliesPrincipal
  {
    #region MyRegion
		public CofamiliesPrincipal(ICofamiliesIdentity identity, IEnumerable<string> roles)
    {
      CofamiliesIdentity = identity;
      Roles = ImmutableList.CreateRange(roles);
    } 
	  #endregion

    // Properties
    
    public IIdentity Identity { get { return CofamiliesIdentity; } }
    public ICofamiliesIdentity CofamiliesIdentity { get; private set; }
    public ImmutableList<string> Roles { get; private set; }

    // Methods

    public bool IsInRole(string role)
    {
      return Roles.Any(x => x.EqualsIgnoreCase(role));
    }
  }
}
