using CourseManager.DataBase.SqlServer.DataAccess;
using CourseManager.Models.Translators;
using CourseManagerServices;
using Microsoft.Extensions.DependencyInjection;

namespace CourseManager
{
  public static class DependencyInjection
  {
    public static void AddDependencyServices(this IServiceCollection services)
    {
      services.AddScoped<IToEntityTranslator, ToEntityTranslator>();
      services.AddScoped<IToDtoTranslator, ToDtoTranslator>();
      services.AddScoped<IServices, Services>();
      services.AddScoped<ICommands, Commands>();
      services.AddTransient<IQueries, Queries>();
    }
  }
}
