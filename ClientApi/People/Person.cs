using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cofamilies.J.Core.People;

namespace Cofamilies.ClientApi.People
{
  public interface IPerson
  { 
  }

  public class Person
  {
    // Constructors

    public Person()
    {
    }

    // Properties

    public string Email { get; set; }
    public string Id { get; set; }
    public bool IsZombie { get; set; }
  }
}
