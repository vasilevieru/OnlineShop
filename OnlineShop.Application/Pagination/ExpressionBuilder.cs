using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace OnlineShop.Application.Pagination
{
    public static class ExpressionBuilder
    {

        public static Expression<Func<TEntity, bool>> WhereChildrenExpression<TEntity>(string[] properties, string filter)
        {
            //left side of lambda: x =>

            ParameterExpression instance = Expression.Parameter(typeof(TEntity), "x"); // x =>
            var childPropertyAccessor = Expression.Property(instance, "children"); // x.children

            var childrenInstance = Expression.Parameter(typeof(TEntity), "y"); // y =>
            var childrenFilter = ChainContainsCallExpression(childrenInstance, properties, filter); // y.field.Contains(filter)
            var childrenLambda = Expression.Lambda<Func<TEntity, bool>>(childrenFilter, childrenInstance);// y => y.field.Contains(filter)

            // x.subItems.Any(y => y.field.Contains(filter));
            var memberCall = Expression.Call(null, GetGenericMethodInfo<TEntity>("Any", 2), childPropertyAccessor, childrenLambda);

            // x.field.Contains(filter);
            var parentFilter = ChainContainsCallExpression(instance, properties, filter);
            // x.subItems.Any(y => y.field.Contains(filter)) || x.field.Contains(filter);
            Expression group = Expression.OrElse(parentFilter, memberCall);

            return Expression.Lambda<Func<TEntity, bool>>(group, instance);
        }

        private static MethodInfo GetGenericMethodInfo<TEntity>(string methodName, int paramNumber)
        {
            var methods = typeof(Enumerable).GetMethods(BindingFlags.Public | BindingFlags.Static);
            var method = methods.First(m => m.Name == methodName && m.GetParameters().Count() == paramNumber);
            return method.MakeGenericMethod(typeof(TEntity));
        }

        public static Expression ChainContainsCallExpression(ParameterExpression instance, string[] properties, string filter) =>
                properties
                .Select(field => ContainsCallExpression(instance, field, filter))   //contains call: x.property.Contains("filter");
                .Aggregate<Expression>(Expression.OrElse);

        public static Expression<Func<TEntity, bool>> WhereExpression<TEntity>(string[] properties, string filter)
        {
            //left side of lambda: x =>
            ParameterExpression instance = Expression.Parameter(typeof(TEntity), "x");

            Expression memberContainsCalls = ChainContainsCallExpression(instance, properties, filter);

            //compose lambda: x => x.property1.Contains("filter") || ...;
            return Expression.Lambda<Func<TEntity, bool>>(memberContainsCalls, instance);
        }

        public static MethodCallExpression ContainsCallExpression(ParameterExpression instance, string field, string filter) =>
            Expression.Call(MemberChainExpression(instance, field), "Contains", null, Expression.Constant(filter));

        public static MemberExpression MemberChainExpression(ParameterExpression instance, string propertyChain)
        {
            var properties = propertyChain.Split('.');
            //build the chain for filters like countryCode.Name
            var memberExpression = Expression.Property(instance, properties[0]);
            for (int i = 1; i < properties.Length; i++)
                memberExpression = Expression.Property(memberExpression, properties[i]);
            return memberExpression;
        }

        public static Expression<Func<TEntity, object>> MemberExpression<TEntity>(string field)
        {
            ParameterExpression argParam = Expression.Parameter(typeof(TEntity), "x");
            Expression property = MemberChainExpression(argParam, field);
            Expression conversion = Expression.Convert(property, typeof(object));
            return Expression.Lambda<Func<TEntity, object>>(conversion, argParam);
        }
    }
}
