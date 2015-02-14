using System.Net;
using Cofamilies.ClientApi.Caching;
using Rob.Interfaces.Core;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Cofamilies.ClientApi
{
  public interface IApiClientContext
  {
    IApiClientCache Cache { get; }
    HttpClient CreateClient();
    AuthenticationHeaderValue GetAuthenticationHeaderValue(IRobID principalRID);
  }

  public class ApiClientContext : IApiClientContext
  {
    // Constructors

    public ApiClientContext(string baseAddress, IApiClientCache cache)
    {
      Cache = cache; // TODO: Empty cache (null pattern)
      Client = BuildHttpClient(baseAddress);
    }

    // Properties

    public IApiClientCache Cache { get; private set; }
    public HttpClient Client { get; private set; }

    // Methods

    private HttpClient BuildHttpClient(string baseAddress)
    {
      var result = new HttpClient {BaseAddress = new Uri(baseAddress)};

      // Add an Accept header for JSON format.
      result.DefaultRequestHeaders.Accept.Add(
          new MediaTypeWithQualityHeaderValue("application/json"));

      return result;
    }

    public HttpClient CreateClient()
    {
      return Client;
    }

    public AuthenticationHeaderValue GetAuthenticationHeaderValue(IRobID principalRID)
    {
      return new AuthenticationHeaderValue(
                    "Basic",
                    Convert.ToBase64String(
                        System.Text.Encoding.ASCII.GetBytes(
                            string.Format("{0}:{1}", "notification.services@dispostable.com", "test")))); //, "unforgiven+notting!7649458436+6955865"))));      
    }
  }
}
