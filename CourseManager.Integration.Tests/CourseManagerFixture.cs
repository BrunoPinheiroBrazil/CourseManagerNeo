using CourseManager.DataBase.SqlServer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CourseManager.Integration.Tests
{
  public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
  {
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
      builder.ConfigureServices(services =>
      {
        services.RemoveAll(typeof(DbContextOptions<CourseManagerDbContext>));
        services.RemoveAll(typeof(CourseManagerDbContext));

        services.AddDbContext<CourseManagerDbContext>((container, options) =>
        {
          //options.UseSqlite("Data Source=:memory:");
          options.UseSqlite("Data Source=Database.db");
        });

        using (var sp = services.BuildServiceProvider())
        {
          var scope = sp.CreateScope();
          var scopedServices = scope.ServiceProvider;
          var db = scopedServices.GetRequiredService<CourseManagerDbContext>();
          var logger = scopedServices
              .GetRequiredService<ILogger<CustomWebApplicationFactory<Program>>>();

          db.Database.OpenConnection();

          db.Database.EnsureCreated();


          try
          {
            Utilities.InitializeDbForTests(db);
          }
          catch (Exception ex)
          {
            logger.LogError(ex, "An error occurred seeding the " +
                "database with test messages. Error: {Message}", ex.Message);
            db.Database.CloseConnection();
          }
        }
      });
      builder.UseEnvironment("Testing");
    }
  }
  public class CourseManagerFixture
  {
    private readonly CustomWebApplicationFactory<Program> _factory;
    public HttpClient _client;

    public CourseManagerFixture()
    {
      _factory = new CustomWebApplicationFactory<Program>();
      _client = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot("./"))
            .CreateClient();
    }

    public async Task<(T ResponseObject, HttpStatusCode StatusCode)> GetInApi<T>(string url)
    {
      var response = await _client.GetAsync(url);
      var responseContent = await response.Content.ReadAsStringAsync();
      var dto = JToken.Parse(responseContent).ToObject<T>();

      return (dto, response.StatusCode);
    }

    public async Task<int> PostInApi(string url, string jsonBody, string source = null)
    {
      var request = new HttpRequestMessage(HttpMethod.Post, url);

      if (!string.IsNullOrEmpty(source))
        request.Headers.Add("source", source);

      request.Content = CreateHttpJsonBody(jsonBody);
      var response = await _client.SendAsync(request);
      return (int)response.StatusCode;
    }

    public async Task<(T ResponseObject, HttpStatusCode StatusCode)> PostInApi<T>(string url, string jsonBody)
    {
      var request = new HttpRequestMessage(HttpMethod.Post, url);

      request.Content = CreateHttpJsonBody(jsonBody);
      var response = await _client.SendAsync(request);
      var responseContent = await response.Content.ReadAsStringAsync();
      var dto = JToken.Parse(responseContent).ToObject<T>();

      return (dto, response.StatusCode);
    }

    public async Task<HttpStatusCode> PutInApi(string url, string jsonBody, string source = null)
    {
      var request = new HttpRequestMessage(HttpMethod.Put, url);

      if (!string.IsNullOrWhiteSpace(source))
        request.Headers.Add("source", source);

      request.Content = CreateHttpJsonBody(jsonBody);
      var response = await _client.SendAsync(request);

      return response.StatusCode;
    }

    public async Task<HttpStatusCode> DeleteInApi(string url)
    {
      var response = await _client.DeleteAsync(url);
      return response.StatusCode;
    }


    public StringContent CreateHttpJsonBody(string jsonContent)
    {
      return new StringContent(jsonContent, Encoding.UTF8, "application/json");
    }
  }
}
