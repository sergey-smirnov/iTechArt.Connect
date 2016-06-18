using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using itechart.PerformanceReview.Data.Helpers;
using Microsoft.CSharp.RuntimeBinder;
using Binder = Microsoft.CSharp.RuntimeBinder.Binder;

namespace itechart.PerformanceReview.Data.Filters
{
    public static class FilterExtensions
    {
        #region filter

        public static IQueryable<TSource> FilterQuery<TSource, TSortFields>(this IQueryable<TSource> source, PagedFilter<TSource, TSortFields> filter)
        {
            if (filter != null)
            {
                source = source.FilterQuery((IBaseFilter<TSource>)filter);

                if (filter.SortBy != null && filter.SortBy.Count() > 0)
                {
                    for (var i = 0; i < filter.SortBy.Count(); i++)
                    {
                        var fieldName = EnumHelper<TSortFields>.GetDescriptionValue(filter.SortBy[i]);
                        if (i == 0)
                        {
                            source = filter.Descending ? source.OrderBy(fieldName) : source.OrderByDescending(fieldName);
                        }
                        else
                        {
                            var sortableSource = (IOrderedQueryable<TSource>)source;
                            source = filter.Descending ? sortableSource.ThenBy(fieldName) : sortableSource.ThenByDescending(fieldName);
                        }
                    }
                }

                if (filter.From.HasValue)
                {
                    source = source.Skip((int)filter.From.Value);
                }

                if (filter.Count.HasValue)
                {
                    source = source.Take((int)filter.Count.Value);
                }
                else if (filter.To.HasValue)
                {
                    var totalCount = source.Count();
                    var takeCount = Math.Min(filter.To.Value, totalCount) - (filter.From ?? 0);
                    source = source.Take((int)takeCount);
                }
            }

            return source;
        }

        public static IQueryable<TSource> FilterQuery<TSource>(this IQueryable<TSource> source, IBaseFilter<TSource> filter)
        {
            return filter != null ? source.Where(filter.ToPredicateExpression()) : source;
        }

        public static async Task<int> FilteredCountAsync<TSource>(this IQueryable<TSource> source, IBaseFilter<TSource> filter)
        {
            return await (filter != null ? source.CountAsync(filter.ToPredicateExpression()) : source.CountAsync());
        }


        #endregion

        #region orderBy IQuerybale

        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder(source, property, "OrderBy");
        }

        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder(source, property, "OrderByDescending");
        }

        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder(source, property, "ThenBy");
        }

        public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder(source, property, "ThenByDescending");
        }

        #region ApplyOrder

        private static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
        {
            string[] props = property.Split('.');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                // use reflection (not ComponentModel) to mirror LINQ
                PropertyInfo pi = type.GetProperty(prop);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<T>)result;
        }

        #endregion

        #endregion

        #region orderBy IEnumerable


        public static IOrderedEnumerable<dynamic> OrderBy(this IEnumerable<dynamic> source, string property)
        {
            return source.OrderBy(AccessorCache.GetAccessor(property), Comparer<object>.Default);
        }
        public static IOrderedEnumerable<dynamic> OrderByDescending(this IEnumerable<dynamic> source, string property)
        {
            return source.OrderByDescending(AccessorCache.GetAccessor(property), Comparer<object>.Default);
        }
        public static IOrderedEnumerable<dynamic> ThenBy(this IOrderedEnumerable<dynamic> source, string property)
        {
            return source.ThenBy(AccessorCache.GetAccessor(property), Comparer<object>.Default);
        }
        public static IOrderedEnumerable<dynamic> ThenByDescending(this IOrderedEnumerable<dynamic> source, string property)
        {
            return source.ThenByDescending(AccessorCache.GetAccessor(property), Comparer<object>.Default);
        }

        #region AccessorCache

        private static class AccessorCache
        {
            private static readonly Hashtable accessors = new Hashtable();

            private static readonly Hashtable callSites = new Hashtable();

            private static CallSite<Func<CallSite, object, object>> GetCallSiteLocked(string name)
            {
                var callSite = (CallSite<Func<CallSite, object, object>>)callSites[name];
                if (callSite == null)
                {
                    callSites[name] = callSite = CallSite<Func<CallSite, object, object>>.Create(
                                Binder.GetMember(CSharpBinderFlags.None, name, typeof(AccessorCache),
                                new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
                }
                return callSite;
            }

            internal static Func<dynamic, object> GetAccessor(string name)
            {
                Func<dynamic, object> accessor = (Func<dynamic, object>)accessors[name];
                if (accessor == null)
                {
                    lock (accessors)
                    {
                        accessor = (Func<dynamic, object>)accessors[name];
                        if (accessor == null)
                        {
                            if (name.IndexOf('.') >= 0)
                            {
                                string[] props = name.Split('.');
                                CallSite<Func<CallSite, object, object>>[] arr = Array.ConvertAll(props, GetCallSiteLocked);
                                accessor = target =>
                                {
                                    object val = (object)target;
                                    for (int i = 0; i < arr.Length; i++)
                                    {
                                        var cs = arr[i];
                                        val = cs.Target(cs, val);
                                    }
                                    return val;
                                };
                            }
                            else
                            {
                                var callSite = GetCallSiteLocked(name);
                                accessor = target =>
                                {
                                    return callSite.Target(callSite, (object)target);
                                };
                            }
                            accessors[name] = accessor;
                        }
                    }
                }
                return accessor;
            }
        }

        #endregion

        #endregion
    }
}