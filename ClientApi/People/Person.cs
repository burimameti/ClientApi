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
  /// A person within cofamilies.  Can be a known identity or a placeholder (i.e., a 'zombie').
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
    /// Indicates whether person has been deleted
    /// </summary>
    bool IsDeleted { get; set; }

    /// <summary>
    /// True if the person is a non-deleted, activated member
    /// </summary>
    bool IsMember { get; }

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
    /// Null or empty if the person is not a member.  Otherwise an ISO 8601 formatted date/time string
    /// representing the initial date and time the person became a member.
    /// </summary>
    string MemberSinceISOString { get; set; }

    /// <summary>
    /// Full name
    /// </summary>
    string Name { get; set; }

    /// <summary>
    /// Time zone information identifier in Olsen database format
    /// </summary>
    string TimeZoneInfoId { get; set; }
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
    public bool IsDeleted { get; set; }

    public bool IsMember
    {
      get { return !string.IsNullOrEmpty(MemberSinceISOString) && !IsDeleted; }
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MemberSinceISOString { get; set; }
    public string Name { get; set; }
    public string TimeZoneInfoId { get; set; }
    public bool IsZombie { get; set; }
  }
}
