using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cofamilies.J.Core.Activities;

namespace Cofamilies.ClientApi.Activities
{
  public class Activities
  {
    public Activities()
    {
      ActivitiesList = new List<Activity>();
    }

    public List<Activity> ActivitiesList { get; private set; } 
  }
}
