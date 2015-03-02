using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Cofamilies.ClientApi.CalendarItems
{
  public interface ICalendarItems
  {
    List<ICalendarItem> CalendarItemsList { get; }
  }

  public class CalendarItems : ICalendarItems
  {
    public CalendarItems()
    {
      CalendarItemsList = new List<ICalendarItem>();
    }

    public List<ICalendarItem> CalendarItemsList { get; private set; }
  }
}
