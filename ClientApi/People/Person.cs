using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cofamilies.J.Core.People;

namespace Cofamilies.ClientApi.People
{
  #region IPerson
  /// <summary>
  /// A person (with known identity or as a placeholder) within cofamilies.
  /// </summary>
  public interface IPerson
  {
    /// <summary>
    /// Email address and user name
    /// </summary>
    string Email { get; set; }

    /// <summary>
    /// First name
    /// </summary>
    string FirstName { get; set; }

    /// <summary>
    /// Unique identifier representing this person
    /// </summary>
    string Id { get; set; }

    /// <summary>
    /// A person with "no identity" is considered a zombie.  For example, if Mom registers
    /// for cofamilies, adds her ex-spouse - but does not invite him - then his person
    /// record is considered a zombie.
    /// 
    /// As soon as he is invited he moves out of zombie status.  However, he will not be
    /// considered a member until he accepts the invitation.
    /// </summary>
    bool IsZombie { get; set; }
    
    /// <summary>
    /// Last name
    /// </summary>
    string LastName { get; set; }

    /// <summary>
    /// Full name
    /// </summary>
    string Name { get; set; }
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
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Name { get; set; }
    public bool IsZombie { get; set; }
  }
}
