using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Cofamilies.ClientApi.Helpers
{
  static class BasicAuthenticationBuilder
  {
    public static AuthenticationHeaderValue Build(string userName)
    {
      return Build(userName, "caucaus+persistent!c17372+0342825375");
    }

    public static AuthenticationHeaderValue Build(string userName, string password)
    {
      return new AuthenticationHeaderValue(
             "Basic",
               Convert.ToBase64String(
                   System.Text.Encoding.ASCII.GetBytes(
                       string.Format("{0}:{1}", userName, password))));
    }
  }
}
