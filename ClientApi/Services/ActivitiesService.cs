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
  public interface IActivitiesService
  {
    ActivityModel Get(IRobID rid);
  }

  public class ActivitiesService : ServiceBase, IActivitiesService
  {
    private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

    // Constructors

    #region ActivitiesService(IApiClientContext context)
    public ActivitiesService(IApiClientContext context)
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

    #region Get(IRobID activityRID)
    public ActivityModel Get(IRobID activityRID)
    {
      var request = BuildRequest("api/activities/{0}".AsFormat(activityRID));
      HttpResponseMessage response = Client.SendAsync(request).Result;
      if (response.IsSuccessStatusCode)
      {
        var jobject = response.Content.ReadAsAsync<JObject>().Result;
        // read past the "activity" wrapper
        var jProperty = (JProperty)jobject.Children().Single();
        return jProperty.Value.ToObject<ActivityModel>();
      }

      Logger.Error("Request {0} returned {1} ({2})", request.RequestUri, (int)response.StatusCode, response.ReasonPhrase);
      // ReSharper disable EmptyGeneralCatchClause
      try
      {
        Logger.Error("Response Body: {0}", response.Content.ReadAsAsync<string>().Result);
      }
      catch (Exception)
      {
      }
      // ReSharper restore EmptyGeneralCatchClause

      Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
      return null;
    } 
    #endregion
  }
}
