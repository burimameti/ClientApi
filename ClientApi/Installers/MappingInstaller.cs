using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Cofamilies.ClientApi.People;
using Cofamilies.J.Core.People;

namespace Cofamilies.ClientApi.Installers
{
  public class MappingInstaller
  {
    public static void Register()
    {
      Mapper.CreateMap<JPerson, Person>();
    }
  }
}
