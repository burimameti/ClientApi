using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Cofamilies.ClientApi.Models;
using Interfaces.ClientApi;
using NLog;
using Newtonsoft.Json.Linq;
using Rob.Core;
using Rob.Interfaces.Core;
using Rob.Interfaces.Core.Data;

namespace Cofamilies.ClientApi.Services
{
  public interface IDeviceService
  {
    List<IDeviceModel> Get(IRobID principalRID);
  }

  public class DevicesService : ServiceBase, IDeviceService
  {
    private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

    // Constructors

    #region DeviceService(IApiClientContext context)
    public DevicesService(IApiClientContext context)
    {
      Context = context;
    }
    #endregion

    // Properties

    protected HttpClient Client
    {
      get { return Context.Client; }
    }

    protected IApiClientContext Context { get; private set; }

    // Methods

    public virtual List<IDeviceModel> Get(IRobID principalRID)
    {
      var request = BuildRequest("api/people/{0}/devices".AsFormat(principalRID));
      HttpResponseMessage response = Client.SendAsync(request).Result;
      var result = new List<IDeviceModel>();
      if (response.IsSuccessStatusCode)
      {
        var jobject = response.Content.ReadAsAsync<JObject>().Result;
        Logger.Debug("Request {0} returned {1}", request.RequestUri, jobject.ToString());
        var jdevices = jobject["devices"] as JArray;
        foreach (JObject jdevice in jdevices)
          result.Add(jdevice.ToObject<DeviceModel>());
      }
      else
      {
        Logger.Error("Request {0} returned {1} ({2})", request.RequestUri, (int)response.StatusCode, response.ReasonPhrase);
        // ReSharper disable EmptyGeneralCatchClause
        try
        {
          Logger.Error("Response Body: {0}", response.Content.ReadAsAsync<string>().Result);
        }
        catch (Exception)
        {
        }
        // ReSharper restore EmptyGeneralCatchClause        
      }

      return result;
    }
  }

  public class CachingDevicesService : DevicesService
  {
    public CachingDevicesService(IApiClientContext context) : base(context)
    {
    }

    public override List<IDeviceModel> Get(IRobID principalRID)
    {
      var key = _ToKey(principalRID);
      var result = Context.Cache.Get<List<IDeviceModel>>(key);
      if (result != null)
        return result;

      result = base.Get(principalRID);
      Context.Cache.Add(result, key);
      return result;
    }

    private string _ToKey(IRobID principalRID)
    {
      return "DevicesService_Get_{0}".AsFormat(principalRID);
    }
  }
}
