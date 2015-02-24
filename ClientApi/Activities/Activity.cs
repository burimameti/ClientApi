using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofamilies.ClientApi.Activities
{
  public class Activity
  {
    public string ActorId { get; set; }
    public DateTime Created { get; set; }
    public string Id { get; set; }
    public string Instance { get; set; }
    public bool IsRemoved { get; set; }
    public DateTime LastModified { get; set; }
    public string ObjectId { get; set; }
    public string Properties { get; set; }
    public string TargetId { get; set; }
    public string Title { get; set; }
    public string Verb { get; set; }
  }
}
