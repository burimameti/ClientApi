using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Cofamilies.ClientApi.Models;
using Newtonsoft.Json.Linq;
using Rob.Core;
using Rob.Interfaces.Core;
using Rob.Interfaces.Core.Data;

namespace Cofamilies.ClientApi.Services
{
  #if refactoring
  public interface IPeopleService
  {
    IPersonModel Get(IRobID rid);
  }

  public class PeopleService : ServiceBase, IPeopleService
  {

    // Constructors

    #region PeopleService(IApiClientContext context)
    public PeopleService(IApiClientContext context)
    {
      Context = context;
    }
    #endregion

    // Properties

    #region Client
    protected HttpClient Client
    {
      get { return Context.Client; }
    } 
    #endregion

    protected IApiClientContext Context { get; private set; }

    // Methods

    #region Get(IRobID rid)
    public virtual IPersonModel Get(IRobID rid)
    {
      var request = BuildRequest("api/people/{0}".AsFormat(rid));
      HttpResponseMessage response = Client.SendAsync(request).Result;
      if (response.IsSuccessStatusCode)
      {
        var jobject = response.Content.ReadAsAsync<JObject>().Result;
        // read past the "person" wrapper
        var jProperty = (JProperty)jobject.Children().Single();
        return jProperty.Value.ToObject<PersonModel>();
      }

      Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
      return null;
    } 
    #endregion
  }

  public class CachingPeopleService : PeopleService
  {
    public CachingPeopleService(IApiClientContext context) : base(context)
    {
    }

    public override IPersonModel Get(IRobID rid)
    {
      var key = _ToKey(rid);
      var result = Context.Cache.Get<IPersonModel>(key);
      if (result != null)
        return result;

      result = base.Get(rid);
      Context.Cache.Add(result, key);
      return result;
    }

    private string _ToKey(IRobID rid)
    {
      return "PeopleService_Get_{0}".AsFormat(rid);
    }
  }
#endif
}
