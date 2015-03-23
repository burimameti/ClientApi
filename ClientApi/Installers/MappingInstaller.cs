using System.Net;
using AutoMapper;
using Cofamilies.ClientApi.Activations;
using Cofamilies.ClientApi.Activities;
using Cofamilies.ClientApi.Devices;
using Cofamilies.ClientApi.People;
using Cofamilies.J.Core.Activations;
using Cofamilies.J.Core.Activities;
using Cofamilies.J.Core.Devices;
using Cofamilies.J.Core.People;

namespace Cofamilies.ClientApi.Installers
{
  public class MappingInstaller
  {
    public static void Register()
    {
      Mapper.CreateMap<JActivity, Activity>();
      Mapper.CreateMap<JActivities, Activities.Activities>()
        .ForMember(d => d.ActivitiesList, opt => opt.MapFrom(s => s.Activities));
      Mapper.CreateMap<JDevice, Device>();
      Mapper.CreateMap<JDevices, Devices.Devices>()
        .ForMember(d => d.DevicesList, opt => opt.MapFrom(s => s.Devices));
      Mapper.CreateMap<JPerson, Person>();
      Mapper.CreateMap<JActivationCreateResult, ActivationCreateResult>();
    }
  }
}
