using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace QueryAggregator
{
    public interface IAggregateResult<T>
    {
        IAggregateResult<TResult> Select<TResult>(Func<T, TResult> selectFunc);
        IAggregateResult<T> Distinct(Func<T,object> distinctOnProperty);
        T[] ToArray();
        IEnumerable<T> ToIEnumerable();
        List<T> ToList();
    }
}