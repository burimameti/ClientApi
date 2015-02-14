using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cofamilies.ClientApiUnitTests.Fakes
{
  public class FakeResponseHandler : DelegatingHandler
  {
    private readonly Dictionary<Uri, HttpResponseMessage> _fakeResponses = new Dictionary<Uri, HttpResponseMessage>();

    public void AddFakeResponse(Uri uri, HttpResponseMessage responseMessage)
    {
      _fakeResponses.Add(uri, responseMessage);
    }

    protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
    {
      if (_fakeResponses.ContainsKey(request.RequestUri))
      {
        return _fakeResponses[request.RequestUri];
      }
      else
      {
        return new HttpResponseMessage(HttpStatusCode.NotFound) { RequestMessage = request };
      }

    }
  }
}
