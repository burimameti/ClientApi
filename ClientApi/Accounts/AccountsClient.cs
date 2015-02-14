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
    Task<JAccountCreateResult> CreateAsync(string email, string name = null, string password = null,
      bool sendActiviationEmail = true, bool acceptTerms = true);
  }
  #endregion

  public class AccountsClient : IAccountsClient
  {
    // Constructors

    #region AccountsClient(IApiClientContext context)
    public AccountsClient(IApiClientContext context)
    {
      Context = context;
    }
    #endregion

    // Properties

    public IApiClientContext Context { get; set; }

    // Methods

    #region CreateAsync(string email, string name = null, string password = null, bool sendActivationEmail = true, bool acceptTerms = true)
    public async Task<JAccountCreateResult> CreateAsync(string email, string name = null, string password = null, bool sendActivationEmail = true, bool acceptTerms = true)
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

      using (var client = Context.CreateClient())
      {
        HttpResponseMessage response = await client.PostAsJsonAsync("api/products/1", jaccount);
        if (response.IsSuccessStatusCode)
          return await response.Content.ReadAsAsync<JAccountCreateResult>();

        response.EnsureSuccessStatusCode();
      }

      // Should never get here

      throw new ApplicationException("Shouldn't be here");
    } 
    #endregion
  }
}
