using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Castle.MicroKernel;
using Rob.Core.DI;
using Rob.Interfaces.Core.DI;

namespace Cofamilies.ClientApi
{
  public interface IApiClientSettings
  {
    string AccountsEndpoint { get; }
    RobFactory<HttpClient> HttpClientFactory { get; set; }
    string Endpoint { get; set; }
  }

  public class ApiClientSettings : IApiClientSettings
  {
    // Statics

    public static readonly IApiClientSettings Default = new ApiClientSettings();

    // Constructors

    public ApiClientSettings()
    {
      HttpClientFactory = new RobFactory<HttpClient>(() => new HttpClient());
    }

    // Properties

    public string AccountsEndpoint
    {
      get { return Endpoint + "/accounts"; }
    }

    public RobFactory<HttpClient> HttpClientFactory { get; set; }

    public string Endpoint
    {
      get
      {
        if (string.IsNullOrEmpty(_endPoint))
          throw new ApplicationException("Api endpoint must be define in ApiClientSettings");

        return _endPoint;
      }
      set { _endPoint = value; }
    }
    private string _endPoint;
  }
}