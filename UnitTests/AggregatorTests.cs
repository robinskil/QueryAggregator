using Microsoft.EntityFrameworkCore;
using QueryAggregator;
using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTests
{
    public class AggregatorTests
    {
        [Fact]
        public void AggregateIncludeTest()
        {
            var context = new QueryTestContext(new DbContextOptions<QueryTestContext>());
            var contextAggregator = new AggregatorContext<QueryTestContext>(new List<QueryTestContext>() { context });
            var query = contextAggregator.CreateQueryable(a => a.Students);
            
        }
    }
}
