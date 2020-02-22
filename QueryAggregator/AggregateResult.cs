using System;
using System.Collections.Generic;
using System.Linq;

namespace QueryAggregator
{
    public class AggregateResult<T> : IAggregateResult<T>
    {
        protected IEnumerable<T> resultData;
        internal AggregateResult(IEnumerable<IQueryable<T>> queryables)
        {
            List<T> data = new List<T>();
            foreach (var query in queryables)
            {
                data.AddRange(query.ToArray());
            }
            resultData = data;
        }
        internal AggregateResult(IEnumerable<T> data)
        {
            resultData = data;
        }
        public List<T> ToList()
        {
            return resultData.ToList();
        }
        public T[] ToArray()
        {
            return resultData.ToArray();
        }
        public IEnumerable<T> ToIEnumerable()
        {
            return resultData;
        }
        public IAggregateResult<TResult> Select<TResult>(Func<T, TResult> selectFunc)
        {
            return new AggregateResult<TResult>(resultData.Select(q => selectFunc(q)));
        }

        public IAggregateResult<T> Distinct(Func<T, object> distinctOnProperty)
        {
            Dictionary<object,T> cachedObjects = new Dictionary<object, T>();
            //add all distinct items to cache
            resultData.Select(i => cachedObjects.TryAdd(distinctOnProperty(i), i));
            return new AggregateResult<T>(cachedObjects.Values);
        }
    }
    public class AggregateResult<T, TOrder> : AggregateResult<T> , IAggregateResult<T>
    {
        private readonly Func<T, TOrder> _order;

        internal AggregateResult(IEnumerable<IQueryable<T>> queryables, Func<T,TOrder> order , bool desc = false) : base(queryables)
        {
            _order = order;
            if (desc)
            {
                HandleOrderByDescending();
            }
            else
            {
                HandleOrderBy();
            }
        }
        private void HandleOrderBy()
        {
            resultData = resultData.OrderBy(_order);
        }
        private void HandleOrderByDescending()
        {
            resultData = resultData.OrderByDescending(_order);
        }
    }
}