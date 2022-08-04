using System;
using System.Linq.Expressions;

namespace Common.Extension.Linq
{
    public static class ExpressionExtension
    {
        public static string PropertyName<T>(this Expression<Func<T, object>> expression)
        {
            if (expression.Body is not MemberExpression body)
            {
                body = ((UnaryExpression)expression.Body).Operand as MemberExpression;
            }

            return body.Member.Name;
        }
        public static MemberExpression Member<T>(this Expression<Func<T, object>> expression)
        {
            if (expression.Body is not MemberExpression member)
            {
                // The property access might be getting converted to object to match the func
                // If so, get the operand and see if that's a member expression
                member = (expression.Body as UnaryExpression)?.Operand as MemberExpression;
            }
            return member ?? throw new ArgumentException("Action must be a member expression.");
        }
    }
}
