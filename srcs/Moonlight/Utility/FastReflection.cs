using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Moonlight.Utility
{
    internal static class FastReflection
    {
        public static Delegate GetPropertySetter(Type type, PropertyInfo propertyInfo)
        {
            ParameterExpression typeExpression = Expression.Parameter(type);
            ParameterExpression parameterExpression = Expression.Parameter(propertyInfo.PropertyType, propertyInfo.Name);
            MemberExpression propertyExpression = Expression.Property(typeExpression, propertyInfo.Name);

            return Expression.Lambda(Expression.Assign(propertyExpression, parameterExpression), typeExpression, parameterExpression).Compile();
        }

        public static Delegate GetPropertyGetter(Type type, PropertyInfo propertyInfo)
        {
            ParameterExpression parameterExpression = Expression.Parameter(type, "value");
            MemberExpression propertyExpression = Expression.Property(parameterExpression, propertyInfo.Name);

            return Expression.Lambda(propertyExpression, parameterExpression).Compile();
        }
    }
}