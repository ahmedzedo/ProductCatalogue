using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ArabDT.Framwork.Reflection.Extensions
{
   public static class ExpressionObjectExtension
    {
        public static string PropertyName<T>(this Expression<Func<T, object>> expression)
        {
            var body = expression.Body as MemberExpression;

            if (body == null)
            {
                body = ((UnaryExpression)expression.Body).Operand as MemberExpression;
            }

            return body.Member.Name;
        }
        public static MemberExpression Member<T>(this Expression<Func<T, object>> expression)
        {
            MemberExpression member = expression.Body as MemberExpression;
          
            if (member == null)
            {
                // The property access might be getting converted to object to match the func
                // If so, get the operand and see if that's a member expression
                member = (expression.Body as UnaryExpression)?.Operand as MemberExpression;
            }
            return member == null ? throw new ArgumentException("Action must be a member expression.") : member;
        }

    }
}
