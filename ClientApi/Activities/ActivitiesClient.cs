using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json.Linq;

namespace Cofamilies.ClientApi.Activities
{
  public interface IActivitiesClient
  {
    Activities GetAll(DateTime? since = null);
    Activity Get(string id);
  }

  public class ActivitiesClient : IActivitiesClient
  {
    public ActivitiesClient(IApiClientSettings settings = null)
    {
      Settings = settings ?? ApiClientSettings.Default;
    }

    // Properties

    public IApiClientSettings Settings { get; private set; }
    public IApiClientContext Context { get; set; }

    #region Endpoint
    public string Endpoint
    {
      get { return Settings.ActivitiesEndpoint; }
    } 
    #endregion

    // Methods

    public Activities GetAll(DateTime? since = null)
    {
      throw new NotImplementedException();
    }

    public Activity Get(string id)
    {
      var task = GetAsync(id);
      return task.GetAwaiter().GetResult();
    }

    public async Task<Activity> GetAsync(string id)
    {
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
        var jresult = jo.SelectToken("activity", false).ToObject<JActivity>();

        // Map

        return MappingEngine.Map<Activity>(jresult);
      }
    }
  }
}
