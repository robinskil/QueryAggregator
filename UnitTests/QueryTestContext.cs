using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    public class QueryTestContext : DbContext
    {
        public QueryTestContext(DbContextOptions<QueryTestContext> dbContextOptions) : base(dbContextOptions)
        {
            var t = Students.AsQueryable();
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }
    }
}
