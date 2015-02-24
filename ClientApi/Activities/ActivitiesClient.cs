using AutoMapper;
using Cofamilies.J.Core.Activities;
using Newtonsoft.Json.Linq;
using Rob.Core;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cofamilies.ClientApi.Activities
{
  public interface IActivitiesClient
  {
    Activity Get(string id);
    Activities GetAll(DateTime? since = null);
    Task<Activities> GetAllAsync(DateTime? since = null);
    Task<Activity> GetAsync(string id);
  }

  public class ActivitiesClient : IActivitiesClient
  {
    public ActivitiesClient(IMappingEngine mappingEngine, IApiClientSettings settings = null)
    {
      MappingEngine = mappingEngine;
      Settings = settings ?? ApiClientSettings.Default;
    }

    // Properties

    public IMappingEngine MappingEngine { get; private set; }
    public IApiClientSettings Settings { get; private set; }
    public IApiClientContext Context { get; set; }

    #region Endpoint
    public string Endpoint
    {
      get { return Settings.ActivitiesEndpoint; }
    } 
    #endregion

    // Methods

    public Activity Get(string id)
    {
      var task = GetAsync(id);
      return task.GetAwaiter().GetResult();
    }

    public Activities GetAll(DateTime? since = null)
    {
      var task = GetAllAsync(since);
      return task.GetAwaiter().GetResult();      
    }

    public async Task<Activities> GetAllAsync(DateTime? since = null)
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

        var jresult = await response.Content.ReadAsAsync<JActivities>();

        // And we're out of here

        return MappingEngine.Map<Activities>(jresult);
      }
    }

    public async Task<Activity> GetAsync(string id)
    {
      // Guard

      if (string.IsNullOrEmpty(id))
        throw new ApplicationException("id cannot be null or empty - use GetAll if getting collection");

      // Create the Url

      var url = Endpoint + "/" + id;

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

        var json = await response.Content.ReadAsStringAsync();
        var jo = JObject.Parse(json);
        var jresult = jo.SelectToken("activity", false).ToObject<JActivity>();

        // Map

        return MappingEngine.Map<Activity>(jresult);
      }
    }
  }
}
