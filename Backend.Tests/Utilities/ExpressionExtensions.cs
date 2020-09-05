using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Backend.Tests.Utilities
{
    public static class ExpressionExtensions
    {
        public static string GetPropertyName<TInput, TResult>(this Expression<Func<TInput, TResult>> expression)
        {
            var memberAccess = expression.Body as MemberExpression;
            var propertyInfo = memberAccess?.Member as PropertyInfo;
            string propertyName = propertyInfo?.Name;
            return propertyName;
        }
    }
}