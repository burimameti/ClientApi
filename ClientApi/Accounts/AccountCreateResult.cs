using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cofamilies.J.Core.Accounts;

namespace Cofamilies.ClientApi.Accounts
{
  public interface IAccountCreateResult
  {
    string ActivationCode { get; set; }
  }

  public class AccountCreateResult : IAccountCreateResult
  {
    public AccountCreateResult(JAccountCreateResult jresult)
    {
      ActivationCode = jresult.ActivationCode;
      PersonId = jresult.PersonId;
    }

    public string ActivationCode { get; set; }
    public string PersonId { get; set; }
  }
}
