using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rob.Core.ChangeSet;

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

    // Methods

    public ChangeSet ChangeSet
    {
      get
      {
        _EnsureChangeSet();
        return _changeSet;
      }
    }
    private ChangeSet _changeSet;

    private void _EnsureChangeSet()
    {
      if (_changeSet != null)
        return;

      var x = Deserialize(Properties) as GenericProperties;
      _changeSet = x.changes;
    }

    #region Deserialize(string s)
    public object Deserialize(string s)
    {
      try
      {
        return JsonConvert.DeserializeObject<GenericProperties>(s);
      }
      catch (Exception)
      {
        return new GenericProperties();
      }
    }
    #endregion
  }
}
