using AutoMapper;
using Cofamilies.ClientApi.Accounts;
using Cofamilies.ClientApi.Activations;
using Cofamilies.ClientApi.Activities;
using Cofamilies.ClientApi.Caching;
using Cofamilies.ClientApi.CalendarItems;
using Cofamilies.ClientApi.Devices;
using Cofamilies.ClientApi.Installers;
using Cofamilies.ClientApi.People;
using System.Net;

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
    IActivationsClient Activations { get; }
    IActivitiesClient Activities { get; }
    ICalendarItemsClient CalendarItems { get; }
    IDevicesClient Devices { get; }
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
      Activations = new ActivationsClient(Mapper.Engine, settings);
      Activities = new ActivitiesClient(Mapper.Engine, settings);
      CalendarItems = new CalendarItemsClient(Mapper.Engine, settings);
      Devices = new DevicesClient(Mapper.Engine, settings);
      People = new PeopleClient(Mapper.Engine, settings);

      //People = new CachingPeopleService(Context);
    } 
    #endregion

    // Properties

    public IAccountsClient Accounts { get; private set; }
    public IActivationsClient Activations { get; private set; }
    public IActivitiesClient Activities { get; private set; }
    public IApiClientCache Cache { get; private set; }
    public ICalendarItemsClient CalendarItems { get; private set; }
    public IApiClientContext Context { get; private set; }
    public IDevicesClient Devices { get; private set; }
    public IPeopleClient People { get; private set; }
  }
}
