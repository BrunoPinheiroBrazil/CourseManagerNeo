using CourseManager.DataBase.SqlServer;
using CourseManager.DataBase.SqlServer.DataAccess;
using CourseManager.Models.Translators;
using CourseManagerServices;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager config = builder.Configuration;

builder.Services.AddControllersWithViews().AddNewtonsoftJson();
builder.Services.AddMvc().AddApplicationPart(typeof(Assembly).Assembly);
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IToEntityTranslator, ToEntityTranslator>();
builder.Services.AddScoped<IToDtoTranslator, ToDtoTranslator>();
builder.Services.AddScoped<IServices, Services>();
builder.Services.AddScoped<ICommands, Commands>();
builder.Services.AddTransient<IQueries, Queries>();

builder.Services.AddDbContext<CourseManagerDbContext>(options =>
{
  if(!options.IsConfigured)
    options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=BrunoEstudos;Trusted_Connection=True;MultipleActiveResultSets=true");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
  app.UseDeveloperExceptionPage();
else
  app.UseExceptionHandler("/Error");

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllerRoute(
  name: "default",
  pattern: "{controller}/{action=Index}/{id?}"
);

using (var svcScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
  var context = svcScope.ServiceProvider.GetRequiredService<CourseManagerDbContext>();
  if (context == null)
    throw new Exception("Could not create DBContext");

  try
  {
    context.Database.Migrate();
  }
  catch (Exception ex) 
  { 
    if(!ex.Message.Contains("already exists"))
      throw new Exception(ex.Message);
  }
}

app.Run();

public partial class Program {}