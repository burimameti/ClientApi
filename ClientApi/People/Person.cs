using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cofamilies.J.Core.People;

namespace Cofamilies.ClientApi.People
{
  #region IPerson
  public interface IPerson
  {
    string Email { get; set; }
    string Id { get; set; }
    bool IsZombie { get; set; }
  }
  #endregion

  public class Person : IPerson
  {
    // Constructors

    #region Person()
    public Person()
    {
    }
    #endregion

    // Properties

    public string Email { get; set; }
    public string Id { get; set; }
    public bool IsZombie { get; set; }
  }
}
