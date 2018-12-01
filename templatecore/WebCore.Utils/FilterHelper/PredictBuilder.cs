using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace WebCore.Utils.FilterHelper
{
    public static class PredictBuilder
    {
        public static Expression<Func<TSource, bool>> GetFilterExpression<TSource, TFilter>(TFilter filter)
        {
            List<ExpressionFilterModel> filters = new List<ExpressionFilterModel>();
            PropertyInfo[] filterProps = typeof(TFilter).GetProperties();
            foreach (PropertyInfo filterProperty in filterProps)
            {
                ExpressionFilterModel expression = new ExpressionFilterModel();
                expression.Comparison = FilterComparison.Equal;
                expression.Value = filterProperty.GetValue(filter);

                if (expression.Value == null)
                {
                    continue;
                }

                FilterAttribute attribute = (FilterAttribute)filterProperty.GetCustomAttributes(true).Where(x => x.GetType() == typeof(FilterAttribute)).FirstOrDefault();
                if (attribute == null || attribute.FilterComparison == FilterComparison.NoFilter)
                {
                    continue;
                }
                expression.Comparison = attribute.FilterComparison;
                if (attribute.FilterProperty != null)
                {
                    expression.PropertyName = attribute.FilterProperty;
                }
                else
                {
                    expression.PropertyName = filterProperty.Name;
                }
                filters.Add(expression);
            }
            return ConstructAndExpressionTree<TSource>(filters);
        }

        public static Expression<Func<T, bool>> ConstructAndExpressionTree<T>(List<ExpressionFilterModel> filters)
        {
            if (filters.Count == 0)
            {
                return null;
            }

            ParameterExpression param = Expression.Parameter(typeof(T), "t");
            Expression exp = null;

            if (filters.Count == 1)
            {
                exp = ExpressionRetriever.GetExpression<T>(param, filters[0]);
            }
            else
            {
                exp = ExpressionRetriever.GetExpression<T>(param, filters[0]);
                for (int i = 1; i < filters.Count; i++)
                {
                    Expression add = ExpressionRetriever.GetExpression<T>(param, filters[i]);
                    if (add != null)
                    {
                        exp = Expression.And(exp, add);
                    }
                }
            }
            return Expression.Lambda<Func<T, bool>>(exp, param);
        }

        public static IQueryable<TModel> Filter<TModel, TFilter>(this IQueryable<TModel> query, TFilter filterObject)
        {
            Expression<Func<TModel, bool>> predict = GetFilterExpression<TModel, TFilter>(filterObject);
            if (predict != null)
            {
                return query.Where(predict);
            }
            return query;
        }
    }
}
