using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Cofamilies.ClientApi.Utilities;
using Cofamilies.J.Core.Accounts;
using Cofamilies.J.Core.People;
using Newtonsoft.Json;
using Rob.Core;

namespace Cofamilies.ClientApi.People
{
  #region IPeopleClient
  public interface IPeopleClient
  {
    Person Get(string id);
    Task<Person> GetAsync(string id);
  }
  #endregion

  public class PeopleClient : IPeopleClient
  {
    // Constructors

    #region PeopleClient(IMappingEngine mappingEngine, IApiClientSettings settings = null)
    public PeopleClient(IMappingEngine mappingEngine, IApiClientSettings settings = null)
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
      get { return Settings.PeopleEndpoint; }
    } 
    #endregion

    public IMappingEngine MappingEngine { get; private set; }
    public IApiClientSettings Settings { get; private set; }

    // Methods

    #region Get(string id)
    public Person Get(string id)
    {
      var task = GetAsync(id);
      return task.GetAwaiter().GetResult();
    }
    #endregion

    #region GetAsync(string id)
    public async Task<Person> GetAsync(string id)
    {
      // Guard

      if (string.IsNullOrEmpty(id))
        throw new ApplicationException("id cannot be null or empty - use GetAll if getting collection");

      // Create the Url

      var url = Endpoint + "/" + id;
      using (var client = Settings.HttpClientFactory.Create())
      {
        var response = await client.GetAsync(url);
        
        if (response.StatusCode == HttpStatusCode.NotFound)
          return null;
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var jresult = JsonConvert.DeserializeObject<JPerson>(json);

        return MappingEngine.Map<Person>(jresult);
      }
    }
    #endregion
  }
}
