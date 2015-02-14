using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Cofamilies.J.Core.Accounts;
using Rob.Core;

namespace Cofamilies.ClientApi.Accounts
{
  #region IAccountsClient
  public interface IAccountsClient
  {
    IAccountCreateResult Create(string email, string name = null, string password = null,
      bool sendActivationEmail = true, bool acceptTerms = true);

    Task<IAccountCreateResult> CreateAsync(string email, string name = null, string password = null,
      bool sendActiviationEmail = true, bool acceptTerms = true);
  }
  #endregion

  public class AccountsClient : IAccountsClient
  {
    // Constructors

    #region AccountsClient(IApiClientSettings settings = null)
    public AccountsClient(IApiClientSettings settings = null)
    {
      Settings = settings ?? ApiClientSettings.Default;
    }
    #endregion

    // Properties

    public IApiClientSettings Settings { get; private set; }
    public IApiClientContext Context { get; set; }

    public string Endpoint
    {
      get { return Settings.AccountsEndpoint; }
    }

    // Methods

    #region CreateAsync(string email, string name = null, string password = null, bool sendActivationEmail = true, bool acceptTerms = true)

    public IAccountCreateResult Create(string email, string name = null, string password = null, bool sendActivationEmail = true,
      bool acceptTerms = true)
    {
      var task = CreateAsync(email, name, password, sendActivationEmail, acceptTerms);
      return task.GetAwaiter().GetResult();
    }

    public async Task<IAccountCreateResult> CreateAsync(string email, string name = null, string password = null, bool sendActivationEmail = true, bool acceptTerms = true)
    {
      // Guard

      if (string.IsNullOrEmpty(password))
        throw new ApplicationException("Password must be specified");

      // Create json to post

      var jaccount = new JAccountCreate
      {
        Email = email,
        Name = string.IsNullOrEmpty(name) ? EmailUtility.SniffName(email) : name,
        Password = password,
        SendActivationEmail = sendActivationEmail,
        AcceptTerms = acceptTerms
      };

      // Post

      JAccountCreateResult jresult = null;
      using (var client = Settings.HttpClientFactory.Create())
      {
        HttpResponseMessage response = await client.PostAsJsonAsync(Endpoint, jaccount);
        if (response.IsSuccessStatusCode)
          jresult = await response.Content.ReadAsAsync<JAccountCreateResult>();

        response.EnsureSuccessStatusCode();
      }

      // Map from wire representation

      return new AccountCreateResult(jresult);
    } 
    #endregion
  }
}
