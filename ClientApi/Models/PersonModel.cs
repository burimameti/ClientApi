using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofamilies.ClientApi.Models
{
  public interface IPersonModel
  {
    string AvatarUrl { get; set; }
    string Email { get; set; }
    string FirstName { get; set; }
    string Id { get; set; }
    DateTime LastModified { get; set; }
    string LastName { get; set; }
    bool IsDeleted { get; set; }
    bool IsZombie { get; set; }
    DateTime MemberSince { get; set; }
    string Role { get; set; }
    string Role_summary { get; set; }
    string TimezoneInfoId { get; set; }
  }

  public class PersonModel : IPersonModel
  {
    public string AvatarUrl { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string Id { get; set; }
    public DateTime LastModified { get; set; }
    public string LastName { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsZombie { get; set; }
    public DateTime MemberSince { get; set; }
    public string PrimaryKey { get { return Id; } }
    public string Role { get; set; }
    public string Role_summary { get; set; }
    public string TimezoneInfoId { get; set; }
  }
}
