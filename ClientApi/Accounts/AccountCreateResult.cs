using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cofamilies.J.Core.Accounts;

namespace Cofamilies.ClientApi.Accounts
{
  #region IAccountCreateResult
  public interface IAccountCreateResult
  {
    string ActivationCode { get; set; }
    string PersonId { get; set; }
  }
  #endregion

  #region AccountCreateResult
  /// <summary>
  /// Represents the returned value of a Post to the Accounts resource.
  /// </summary>
  public class AccountCreateResult : IAccountCreateResult
  {
    // Constructors

    public AccountCreateResult(JAccountCreateResult jresult)
    {
      ActivationCode = jresult.ActivationCode;
      PersonId = jresult.PersonId;
    }

    // Properties

    public string ActivationCode { get; set; }
    public string PersonId { get; set; }
  }
  #endregion
}
