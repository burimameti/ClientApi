using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Cofamilies.ClientApi.Services
{
  public abstract class ServiceBase
  {
    protected HttpRequestMessage BuildRequest(string endpoint)
    {
      var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
      request.Headers.Authorization =
          new AuthenticationHeaderValue(
              "Basic",
              Convert.ToBase64String(
                  System.Text.Encoding.ASCII.GetBytes(
                            string.Format("{0}:{1}", "notification.services@dispostable.com", "test"))));

      return request;
    }
  }
}
