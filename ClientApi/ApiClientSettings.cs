using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Castle.MicroKernel;
using Rob.Core;
using Rob.Core.DI;
using Rob.Interfaces.Core.DI;

namespace Cofamilies.ClientApi
{
  public interface IApiClientSettings
  {
    string AccountsEndpoint { get; }
    string ActivationsEndpoint { get; }
    string ActivitiesEndpoint { get; }
    string CalendarItemsEndpoint { get; }
    string DevicesEndpoint { get; }
    RobFactory<HttpClient> HttpClientFactory { get; set; }
    string UserName { get; set; }
    string Password { get; set; }
    string Endpoint { get; set; }
    string PeopleEndpoint { get; }
  }

  public class ApiClientSettings : IApiClientSettings
  {
    // Statics

    public static readonly IApiClientSettings Default = new ApiClientSettings();

    // Constructors

    #region ApiClientSettings()
    public ApiClientSettings()
    {
      HttpClientFactory = new RobFactory<HttpClient>(CreateHttpClient);
    } 
    #endregion

    // Properties

    public string AccountsEndpoint
    {
      get { return Endpoint + "/accounts"; }
    }

    public string ActivationsEndpoint
    {
      get { return Endpoint + "/activations"; }
    }

    public string ActivitiesEndpoint
    {
      get { return Endpoint + "/activities"; }
    }

    public string CalendarItemsEndpoint
    {
      get { return Endpoint + "/calendaritems"; }
    }

    public string DevicesEndpoint
    {
      get { return Endpoint + "/devices"; }
    }

    public RobFactory<HttpClient> HttpClientFactory { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }

    #region Endpoint
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
    #endregion

    #region PeopleEndpoint
    public string PeopleEndpoint
    {
      get { return Endpoint + "/people"; }
    } 
    #endregion

    // Methods

    #region CreateHttpClient()
    protected HttpClient CreateHttpClient()
    {
      var result = new HttpClient();
      if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
        return result;

      var byteArray = Encoding.ASCII.GetBytes("{0}:{1}".AsFormat(UserName, Password));
      result.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
      return result;
    } 
    #endregion
  }
}