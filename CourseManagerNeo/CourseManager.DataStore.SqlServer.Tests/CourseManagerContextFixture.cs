using CourseManager.DataBase.SqlServer;
using Microsoft.EntityFrameworkCore;
using System;

namespace CourseManager.DataStore.SqlServer.Tests
{
  public class CourseManagerContextFixture : IDisposable
  {
    public CourseManagerDbContext Context { get; private set; }

    public CourseManagerContextFixture()
    {
      var options = new DbContextOptionsBuilder<CourseManagerDbContext>()
            .UseInMemoryDatabase("CourseManagerCommandsTestDb")
            .Options;

      Context = new CourseManagerDbContext(options);

      Context.Database.EnsureCreated();
    }

    public void Dispose()
    {
      Context.Dispose();
    }
  }
}
