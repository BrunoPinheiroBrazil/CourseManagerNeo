using CourseManager.DataBase.SqlServer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManager.Integration.Tests
{
  public class FakeDbContext : CourseManagerDbContext
  {
    public FakeDbContext(DbContextOptions<CourseManagerDbContext> options) : base(options) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      base.OnConfiguring(optionsBuilder);
    }
  }
}
