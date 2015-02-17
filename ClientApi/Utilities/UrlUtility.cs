using System;

namespace Cofamilies.ClientApi.Utilities
{
  public static class UrlUtility
  {
    public static string Combine(string first, string second)
    {
      var baseUri = new Uri(first);
      var result = new Uri(baseUri, second);

      return result.ToString();
    }
  }
}
