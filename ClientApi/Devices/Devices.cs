using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofamilies.ClientApi.Devices
{
  public class Devices
  {
    public Devices()
    {
      DevicesList = new List<Device>();
    }

    public List<Device> DevicesList { get; private set; }
  }
}
