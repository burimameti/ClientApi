using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using Cofamilies.ClientApi.Models;
using Rob.Core;

namespace Cofamilies.ClientApi.Caching
{
  public interface IApiClientCache
  {
    void Add<TItem>(TItem model, string cacheKey) where TItem : class;
    TItem Get<TItem>(string cacheKey) where TItem : class;
  }

  public class ApiClientCache : MemoryCache, IApiClientCache
  {
    // Constructors

    #region ApiClientCache()
    public ApiClientCache() : base("ApiClient")
    {
      //CacheItemPolicy policy = new CacheItemPolicy();
      //policy.SlidingExpiration = new TimeSpan(4, 0, 0);
      //policy.UpdateCallback = UpdateCallback;
      //ModelPolicy = policy;
    }
    #endregion

    // Properties

    //protected CacheItemPolicy ModelPolicy { get; private set; }

    // Methods

    #region Add<TItem>(TItem model, string cacheKey)
    public void Add<TItem>(TItem model, string cacheKey) where TItem : class
    {
      if (Contains(cacheKey))
        return;

      // don't cache nulls
      if (model == null)
        return;

      Set(cacheKey, model, _Policy());
    } 
    #endregion

    #region Get<TItem>(string cacheKey) where TItem : class
    public TItem Get<TItem>(string cacheKey) where TItem : class
    {
      return Get(cacheKey) as TItem;
    } 
    #endregion

    #region _Policy()
    private CacheItemPolicy _Policy()
    {
      var result = new CacheItemPolicy();
      result.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(10);
      result.UpdateCallback = UpdateCallback;
      return result;
    } 
    #endregion

    #region UpdateCallback(CacheEntryUpdateArguments arguments)
    private void UpdateCallback(CacheEntryUpdateArguments arguments)
    {
    } 
    #endregion
  }
}
