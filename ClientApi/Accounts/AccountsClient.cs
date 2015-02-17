using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Cofamilies.ClientApi.Utilities;
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
      bool sendActivationEmail = true, bool acceptTerms = true);

    bool ResetPassword(string email);
    Task<bool> ResetPasswordAsync(string email);
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

    #region Endpoint
    public string Endpoint
    {
      get { return Settings.AccountsEndpoint; }
    } 
    #endregion

    // Methods

    #region Create(string email, string name = null, string password = null, bool sendActivationEmail = true, bool acceptTerms = true)
    public IAccountCreateResult Create(string email, string name = null, string password = null, bool sendActivationEmail = true, bool acceptTerms = true)
    {
      var task = CreateAsync(email, name, password, sendActivationEmail, acceptTerms);
      return task.GetAwaiter().GetResult();
    } 
    #endregion

    #region CreateAsync(string email, string name = null, string password = null, bool sendActivationEmail = true, bool acceptTerms = true)
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

    #region ResetPassword(string email)
    public bool ResetPassword(string email)
    {
      var task = ResetPasswordAsync(email);
      return task.GetAwaiter().GetResult();
    } 
    #endregion

    #region ResetPasswordAsync(string email)
    public async Task<bool> ResetPasswordAsync(string email)
    {
      var url = Endpoint + "/{0}/password_resets".AsFormat(email);
      using (var client = Settings.HttpClientFactory.Create())
      {
        HttpResponseMessage response = await client.PostAsync(url, new StringContent(""));

        if (response.IsSuccessStatusCode)
          return true;

        if (response.StatusCode == HttpStatusCode.NotFound)
          return false;

        response.EnsureSuccessStatusCode();
      }

      throw new ApplicationException("should never get here");
    } 
    #endregion
  }
}
