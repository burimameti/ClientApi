using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using Cofamilies.ClientApi.Caching;
using Cofamilies.ClientApi.Services;

namespace Cofamilies.ClientApi
{
  public interface IApiClient
  {
    IActivitiesService Activities { get; }
    IApiClientContext Context { get; }
    IDeviceService Devices { get; }
    IPeopleService People { get; }    
  }

  public class ApiClient : IApiClient
  {
    // Constructor

    #region ApiClient()
    public ApiClient(IApiClientCache cache, IApiClientContext context)
    {
//#if DEBUG
      ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
//#endif
      Cache = cache;
      Context = context;

      Activities = new ActivitiesService(Context);
      Devices = new CachingDevicesService(Context);
      People = new CachingPeopleService(Context);
    } 
    #endregion

    // Properties

    public IActivitiesService Activities { get; private set; }
    public IApiClientCache Cache { get; private set; }
    public IApiClientContext Context { get; private set; }
    public IDeviceService Devices { get; private set; }
    public IPeopleService People { get; private set; }
  }
}
