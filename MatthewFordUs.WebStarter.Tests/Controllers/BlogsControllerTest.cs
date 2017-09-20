/*   Copyright 2017 Matthew Ford <matthew@matthewford.us>
 *
 *   Licensed under the Apache License, Version 2.0 (the "License");
 *   you may not use this file except in compliance with the License.
 *   You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 *   Unless required by applicable law or agreed to in writing, software
 *   distributed under the License is distributed on an "AS IS" BASIS,
 *   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *   See the License for the specific language governing permissions and
 *   limitations under the License.
 */

using System.Net;
using System.Net.Http;
using System.Text;
using FluentAssertions;
using MatthewFordUs.WebStarter.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace MatthewFordUs.WebStarter.Tests.Controllers
{
  public abstract class TestBase
  {
    private readonly TestServer _server;
    protected readonly HttpClient Client;

    protected TestBase()
    {
      _server = new TestServer(new WebHostBuilder()
        .UseStartup<Startup>());
      Client = _server.CreateClient();
    }
  }

  public class BlogsControllerTest : TestBase
  {
    [Fact]
    public async void Get_Should_ReturnEmptyList()
    {
      var response = await Client.GetAsync("/api/blogs");
      var responseString = await response.Content.ReadAsStringAsync();

      response.StatusCode.Should().Be(HttpStatusCode.OK);
      responseString.Should().Be("[]");
    }
  }
}
