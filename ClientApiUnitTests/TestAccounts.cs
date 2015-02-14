using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Cofamilies.ClientApi;
using Cofamilies.ClientApi.Accounts;
using Cofamilies.ClientApiUnitTests.Fakes;
using NUnit.Framework;
using Rob.Core;
using Rob.Core.DI;

namespace Cofamilies.ClientApiUnitTests
{
  [TestFixture]
  public class TestAccounts
  {
    // Creation Methods

    public string CreateEmail()
    {
      return "test.".Random(6) + "@dispostable.com";
    }

    public HttpClient CreateFakeHttpClient()
    {
      var fakeResponseHandler = new FakeResponseHandler();
      fakeResponseHandler.AddFakeResponse(new Uri(ApiClientSettings.Default.Endpoint + "/accounts"), new HttpResponseMessage(HttpStatusCode.OK));

      return new HttpClient(fakeResponseHandler);
    }

    // Scaffolding

    [SetUp]
    public void SetUp()
    {
      //ApiClientSettings.Default.Endpoint = "https://cofamiliesmaster.azurewebsites.net/api";
      ApiClientSettings.Default.Endpoint = "https://localhost:44301/api";
      ApiClientSettings.Default.HttpClientFactory = new RobFactory<HttpClient>(CreateFakeHttpClient);

      // TODO: Resume dealing with mocking hell at a later date

      //var response = new HttpResponseMessage(HttpStatusCode.OK);
      //response.Content = HttpContent.
      //"{ activationCode: foo }";

      var fakeHandler = new FakeResponseHandler();
      fakeHandler.AddFakeResponse(new Uri(ApiClientSettings.Default.AccountsEndpoint), new HttpResponseMessage(HttpStatusCode.OK));
    }

    // Test Cases

    [Ignore("Mocking HttpResponse too low of an ROI, prefer integration level tests")]
    public void Create()
    {
      // Arrange

      var account = new AccountsClient();

      // Act

      var result = account.Create(CreateEmail(), null, "test");

      // Assert

      Assert.IsNotNull(result);
      Assert.IsNotNullOrEmpty(result.ActivationCode);
    }
  }
}
