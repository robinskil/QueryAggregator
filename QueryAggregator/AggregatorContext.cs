using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QueryAggregator
{
    public sealed class AggregatorContext<TContext>
    {
        private readonly ICollection<TContext> _contexts;

        public AggregatorContext(ICollection<TContext> contexts)
        {
            _contexts = contexts;
        }
        
        public AggregateQueryable<T> CreateQueryable<T>(Func<TContext,DbSet<T>> queryable) where T : class
        {
            IEnumerable<IQueryable<T>> queryables = _contexts.Select(s => queryable(s).AsQueryable());
            return new AggregateQueryable<T>(queryables);
        }
    }
}
