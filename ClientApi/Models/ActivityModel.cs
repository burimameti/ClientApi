using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rob.Core.ChangeSet;

namespace Cofamilies.ClientApi.Models
{
  public interface IActivityModel
  {
    string ActorId { get; set; }
    DateTime Created { get; set; }
    string Id { get; set; }
    string Instance { get; set; }
    bool IsRemoved { get; set; }
    DateTime LastModified { get; set; }
    string ObjectId { get; set; }
    string Properties { get; set; }
    string TargetId { get; set; }
    string Title { get; set; }
    string Verb { get; set; }
  }

  public class ActivityModel : IActivityModel
  {
    // Mapped

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

    // Derived

    #region AbsoluteId
    public string AbsoluteId
    {
      get
      {
        if (string.IsNullOrEmpty(ObjectId))
          return "";

        if (string.IsNullOrEmpty(Instance))
          return ObjectId;

        return ObjectId + "-" + Instance;
      }
    } 
    #endregion

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
