using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofamilies.ClientApi.Models
{
  public interface IDeviceModel : IClientModel
  {
    DateTime Created { get; set; }
    string Id { get; set; }
    string Name { get; set; }
    string Os { get; set; }
    string Status { get; set; }
    string Token { get; set; }
  }

  public class DeviceModel : IDeviceModel
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
