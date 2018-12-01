using System;
using System.Linq.Expressions;
using System.Reflection;

namespace WebCore.Utils.FilterHelper
{
    public static class ExpressionRetriever
    {
        private static readonly MethodInfo containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
        private static readonly MethodInfo startsWithMethod = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
        private static readonly MethodInfo endsWithMethod = typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });

        private static void FilterForNullableMember(ref Expression e1, ref Expression e2)
        {
            if (IsNullableType(e1.Type) && !IsNullableType(e2.Type))
            {
                e2 = Expression.Convert(e2, e1.Type);
            }
            else if (!IsNullableType(e1.Type) && IsNullableType(e2.Type))
            {
                e1 = Expression.Convert(e1, e2.Type);
            }
        }

        private static bool IsNullableType(Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static Expression GetExpression<T>(ParameterExpression param, ExpressionFilterModel filter)
        {
            if (filter.Value == null)
            {
                return null;
            }

            Expression member = Expression.Property(param, filter.PropertyName);
            Expression constant = Expression.Constant(filter.Value);

            if (member.Type.Name.ToLower() != "string")
            {
                FilterForNullableMember(ref member, ref constant);
            }

            BinaryExpression exp = Expression.NotEqual(member, Expression.Constant(null));
            Expression next = null;
            switch (filter.Comparison)
            {
                case FilterComparison.Equal:
                    next = Expression.Equal(member, constant);
                    break;
                case FilterComparison.GreaterThan:
                    next = Expression.GreaterThan(member, constant);
                    break;
                case FilterComparison.GreaterThanOrEqual:
                    next = Expression.GreaterThanOrEqual(member, constant);
                    break;
                case FilterComparison.LessThan:
                    next = Expression.LessThan(member, constant);
                    break;
                case FilterComparison.LessThanOrEqual:
                    next = Expression.LessThanOrEqual(member, constant);
                    break;
                case FilterComparison.NotEqual:
                    next = Expression.NotEqual(member, constant);
                    break;
                case FilterComparison.Contains:
                    constant = Expression.Constant(((string)filter.Value).ToLower());
                    next = Expression.Call(member, containsMethod, constant);
                    break;
                case FilterComparison.StartsWith:
                    constant = Expression.Constant(((string)filter.Value).ToLower());
                    next = Expression.Call(member, startsWithMethod, constant);
                    break;
                case FilterComparison.EndsWith:
                    constant = Expression.Constant(((string)filter.Value).ToLower());
                    next = Expression.Call(member, endsWithMethod, constant);
                    break;
                default:
                    return null;
            }
            return Expression.And(exp, next);
        }
    }
}
