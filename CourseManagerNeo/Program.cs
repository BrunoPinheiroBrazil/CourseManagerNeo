using CourseManager;
using CourseManager.DataBase.SqlServer;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager config = builder.Configuration;

builder.Services.AddControllersWithViews().AddNewtonsoftJson();
builder.Services.AddMvc().AddApplicationPart(typeof(Assembly).Assembly);
builder.Services.AddDependencyServices();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CourseManagerDbContext>(options =>
{
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
  var context = svcScope.ServiceProvider.GetService<CourseManagerDbContext>();
  if (context == null)
    throw new Exception("Could not create DBContext");

  context.Database.Migrate();
}

app.Run();

public partial class Program { }