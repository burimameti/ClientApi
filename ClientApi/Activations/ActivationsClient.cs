using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Cofamilies.J.Core.Activations;
using Newtonsoft.Json.Linq;

namespace Cofamilies.ClientApi.Activations
{
  public interface IActivationsClient
  {
    ActivationCreateResult Create(string id);
    Task<ActivationCreateResult> CreateAsync(string id);
  }

  public class ActivationsClient : IActivationsClient
  {
    // Constructors

    #region ActivationsClient(IMappingEngine mappingEngine, IApiClientSettings settings = null)
    public ActivationsClient(IMappingEngine mappingEngine, IApiClientSettings settings = null)
    {
      MappingEngine = mappingEngine;
      Settings = settings;
    } 
    #endregion

    // Properties

    public IApiClientSettings Settings { get; private set; }

    #region Endpoint
    public string Endpoint
    {
      get { return Settings.AccountsEndpoint; }
    }
    #endregion

    public IMappingEngine MappingEngine { get; private set; }

    // Methods

    #region Create(string id)
    public ActivationCreateResult Create(string id)
    {
      var task = CreateAsync(id);
      return task.GetAwaiter().GetResult();  
    } 
    #endregion

    #region CreateAsync(string id)
    public async Task<ActivationCreateResult> CreateAsync(string id)
    {
      // Guard

      if (string.IsNullOrEmpty(id))
        throw new ApplicationException("id must be specified");

      // Create json to post - currently empty

      var jactivation = new JActivationCreate();

      // Post

      JActivationCreateResult jresult = null;
      using (var client = Settings.HttpClientFactory.Create())
      {
        HttpResponseMessage response = await client.PostAsJsonAsync(Endpoint, jactivation);
        if (response.IsSuccessStatusCode)
          jresult = await response.Content.ReadAsAsync<JActivationCreateResult>();

        response.EnsureSuccessStatusCode();
      }

      // Map from wire representation

      return MappingEngine.Map<ActivationCreateResult>(jresult);
    } 
    #endregion
  }
}
