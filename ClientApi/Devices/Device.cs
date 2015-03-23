using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofamilies.ClientApi.Devices
{
  public interface IDevice
  {
    DateTime Created { get; set; }
    string Id { get; set; }
    string Name { get; set; }
    string Os { get; set; }
    string Status { get; set; }
    string Token { get; set; }
  }

  public class Device : IDevice
  {
    public DateTime Created { get; set; }
    public string Id { get; set; }
    public string Name { get; set; }
    public string Os { get; set; }
    public string PrimaryKey { get { return Id; } }
    public string Status { get; set; }
    public string Token { get; set; }
  }
}


