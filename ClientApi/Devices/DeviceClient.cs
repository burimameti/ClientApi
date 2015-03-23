using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Cofamilies.J.Core.Devices;
using Newtonsoft.Json.Linq;
using Rob.Core;

namespace Cofamilies.ClientApi.Devices
{
  #region IDeviceClient
  public interface IDevicesClient
  {
    Device Get(string id);
    Task<Device> GetAsync(string id);
  }
  #endregion

  public class DevicesClient : IDevicesClient
  {
    // Constructors

    #region DevicesClient(IMappingEngine mappingEngine, IApiClientSettings settings = null)

    public DevicesClient(IMappingEngine mappingEngine, IApiClientSettings settings = null)
    {
      MappingEngine = mappingEngine;
      Settings = settings ?? ApiClientSettings.Default;
    }

    #endregion

    // Properties

    public IApiClientContext Context { get; set; }

    #region Endpoint

    public string Endpoint
    {
      get { return Settings.DevicesEndpoint; }
    }

    #endregion

    public IMappingEngine MappingEngine { get; private set; }
    public IApiClientSettings Settings { get; private set; }

    // Methods

    #region Get(string id)

    public Device Get(string id)
    {
      var task = GetAsync(id);
      return task.GetAwaiter().GetResult();
    }

    #endregion

    #region GetAsync(string id)

    public async Task<Device> GetAsync(string id)
    {
      // Guard

      if (string.IsNullOrEmpty(id))
        throw new ApplicationException("id cannot be null or empty - use GetAll if getting collection");

      // Create the Url

      var url = Endpoint + "/" + id;
      using (var client = Settings.HttpClientFactory.Create())
      {
        // Read

        var response = await client.GetAsync(url);

        // Guard

        if (response.StatusCode == HttpStatusCode.NotFound)
          return null;
        response.EnsureSuccessStatusCode();

        // Convert

        var json = await response.Content.ReadAsStringAsync();
        var jo = JObject.Parse(json);
        var jresult = jo.SelectToken("device", false).ToObject<JDevice>();

        // Map

        return MappingEngine.Map<Device>(jresult);
      }
    }
    #endregion


    public Devices GetAll(DateTime? since = null)
    {
      var task = GetAllAsync(since);
      return task.GetAwaiter().GetResult();
    }

    public async Task<Devices> GetAllAsync(DateTime? since = null)
    {
      // Create the Url

      var url = Endpoint + "/";
      if (since != null)
        url = url + "?since=" + since.Value.ToISO8601();

      // Get

      using (var client = Settings.HttpClientFactory.Create())
      {
        // Read

        var response = await client.GetAsync(url);

        // Guard

        if (response.StatusCode == HttpStatusCode.NotFound)
          return null;
        response.EnsureSuccessStatusCode();

        // Convert

        var jresult = await response.Content.ReadAsAsync<JDevices>();

        // And we're out of here

        return MappingEngine.Map<Devices>(jresult);
      }
    }
  }
}
