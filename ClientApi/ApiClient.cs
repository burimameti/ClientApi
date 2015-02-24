using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Cofamilies.ClientApi.Accounts;
using Cofamilies.ClientApi.Activities;
using Cofamilies.ClientApi.Caching;
using Cofamilies.ClientApi.Installers;
using Cofamilies.ClientApi.People;
using Cofamilies.ClientApi.Services;

namespace Cofamilies.ClientApi
{
  /// <summary>
  /// Facade for ApiClient
  /// </summary>
  /// <remarks>Various representations</remarks>
  public interface IApiClient
  {
    /// <summary>
    /// Accounts representation.
    /// </summary>
    /// <remarks>Requires registered application token or whitelisted IP address</remarks>
    IAccountsClient Accounts { get; }
    IActivitiesClient Activities { get; }
    IApiClientContext Context { get; }
    //IDeviceService Devices { get; }
    /// <summary>
    /// People representation
    /// </summary>
    IPeopleClient People { get; }    
  }

  public class ApiClient : IApiClient
  {
    // Constructor

    #region ApiClient()
    //public ApiClient(IApiClientCache cache, IApiClientContext context)
    public ApiClient(IApiClientSettings settings = null)
    {
      // Settings

      settings = settings ?? ApiClientSettings.Default;

      // Mapping

      MappingInstaller.Register();

//#if DEBUG
      ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
//#endif
      //Cache = cache;
      //Context = context;

      Accounts = new AccountsClient(settings);
      Activities = new ActivitiesClient(settings);
      People = new PeopleClient(Mapper.Engine, settings);

      //Devices = new CachingDevicesService(Context);
      //People = new CachingPeopleService(Context);
    } 
    #endregion

    // Properties

    public IAccountsClient Accounts { get; private set; }
    public string ApiAddress { get; private set; }
    public IActivitiesClient Activities { get; private set; }
    public IApiClientCache Cache { get; private set; }
    public IApiClientContext Context { get; private set; }
    //public IDeviceService Devices { get; private set; }
    public IPeopleClient People { get; private set; }
  }
}
