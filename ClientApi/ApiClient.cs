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
using Cofamilies.ClientApi.Caching;
using Cofamilies.ClientApi.Installers;
using Cofamilies.ClientApi.People;
using Cofamilies.ClientApi.Services;

namespace Cofamilies.ClientApi
{
  public interface IApiClient
  {
    IAccountsClient Accounts { get; }
    //IActivitiesService Activities { get; }
    IApiClientContext Context { get; }
    //IDeviceService Devices { get; }
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
      People = new PeopleClient(Mapper.Engine, settings);
      //Activities = new ActivitiesService(Context);
      //Devices = new CachingDevicesService(Context);
      //People = new CachingPeopleService(Context);
    } 
    #endregion

    // Properties

    public IAccountsClient Accounts { get; private set; }
    public string ApiAddress { get; private set; }
    //public IActivitiesService Activities { get; private set; }
    public IApiClientCache Cache { get; private set; }
    public IApiClientContext Context { get; private set; }
    //public IDeviceService Devices { get; private set; }
    public IPeopleClient People { get; private set; }
  }
}
