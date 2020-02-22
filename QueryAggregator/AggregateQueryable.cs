using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace QueryAggregator
{
    public class AggregateQueryable<T> where T : class
    {
        protected virtual IEnumerable<IQueryable<T>> Queryables { get; set; }
        internal AggregateQueryable(IEnumerable<IQueryable<T>> queryables)
        {
            Queryables = queryables;
        }
        public AggregateIncludedQueryable<T,TProperty> Include<TProperty>(Expression<Func<T,TProperty>> func)
        {
            var t = Queryables.First().Include(func);
            return new AggregateIncludedQueryable<T, TProperty>(Queryables.Select(q => q.Include(func)));
        }
        public void Where(Expression<Func<T,bool>> where)
        {
            Queryables = Queryables.Select(q => q.Where(where));
        }
        public void Build(Func<IQueryable<T> , IQueryable<T>> build)
        {
            Queryables = Queryables.Select(q => build(q));
        }
        public IAggregateResult<T> OrderBy<TOrder>(Expression<Func<T,TOrder>> orderExpression)
        {
            return new AggregateResult<T, TOrder>(Queryables.Select(q => q.OrderBy(orderExpression)),orderExpression.Compile());
        }
        public IAggregateResult<T> OrderByDescending<TOrder>(Expression<Func<T, TOrder>> orderExpression)
        {
            return new AggregateResult<T, TOrder>(Queryables.Select(q => q.OrderByDescending(orderExpression)), orderExpression.Compile() , true);
        }
        public IAggregateResult<T> ToResult()
        {
            return new AggregateResult<T>(Queryables);
        }
    }

    public class AggregateIncludedQueryable<T, TIncluded> : AggregateQueryable<T> where T : class
    {
        private IEnumerable<IIncludableQueryable<T, TIncluded>> _includedQueryables;
        protected override IEnumerable<IQueryable<T>> Queryables { get
            {
                return _includedQueryables;
            } 
        }
        internal AggregateIncludedQueryable(IEnumerable<IIncludableQueryable<T, TIncluded>> queryables) : base(queryables)
        {
            _includedQueryables = queryables;
        }
        public AggregateIncludedQueryable<T,TProperty> ThenInclude<TProperty>(Expression<Func<TIncluded, TProperty>> func)
        {
            return new AggregateIncludedQueryable<T, TProperty>(_includedQueryables.Select(q => q.ThenInclude(func)));
        }
    }
}
