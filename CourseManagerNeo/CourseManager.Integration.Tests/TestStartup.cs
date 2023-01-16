using CourseManager.DataBase.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CourseManager.Integration.Tests
{
  public class TestStartup : Startup
  {
    public new IConfiguration Configuration { get; }

    public TestStartup(IConfiguration configuration) : base(configuration)
    {
      Configuration = configuration;
    }
    protected override void ConfigureDb(IServiceCollection services)
    {
      var controllersAssemblyType = typeof(Startup).GetTypeInfo().Assembly;
      services.AddMvc().AddApplicationPart(controllersAssemblyType);

      var serviceProvider = services.BuildServiceProvider();
      var options = serviceProvider.GetService<DbContextOptions<CourseManagerDbContext>>();
      services.AddScoped(s => new FakeDbContext(options));
    }
    protected override void MigrateDB(IApplicationBuilder app){}
  }
}
