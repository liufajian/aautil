using System.Linq.Expressions;

namespace System.Reflection
{
    /// <summary>
    /// Lambda表达式应用
    /// </summary>
    public static class ExpressionUtil
    {
        public static Func<TObject, TProperty> GetPropGetter<TObject, TProperty>(string propertyName)
        {
            var value = Expression.Parameter(typeof(TObject), "value");
            var prop = Expression.Property(value, propertyName);
            return Expression.Lambda<Func<TObject, TProperty>>(prop, value).Compile();
        }

        public static Func<TEntity, TProperty> GetPropGetter<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> property)
        {
            var prop = GetProperty(property);
            var obj = Expression.Parameter(typeof(TEntity), "instance");
            var body = Expression.Call(obj, prop.GetGetMethod());
            var para = new ParameterExpression[] { obj };
            return Expression.Lambda<Func<TEntity, TProperty>>(body, para).Compile();
        }

        public static Action<TObject, TProperty> GetPropSetter<TObject, TProperty>(string propertyName)
        {
            var obj = Expression.Parameter(typeof(TObject));
            var value = Expression.Parameter(typeof(TProperty), propertyName);
            var prop = Expression.Property(obj, propertyName);
            return Expression.Lambda<Action<TObject, TProperty>>
            (
                Expression.Assign(prop, value), obj, value
            ).Compile();
        }

        public static Action<TEntity, TProperty> GetPropSetter<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> property)
        {
            var pinfo = GetProperty(property);
            var instance = Expression.Parameter(typeof(TEntity), "instance");
            var para = Expression.Parameter(typeof(TProperty), "param");
            var body = Expression.Call(instance, pinfo.GetSetMethod(), para);
            var paras = new ParameterExpression[] { instance, para };
            return Expression.Lambda<Action<TEntity, TProperty>>(body, paras).Compile();
        }

        public static Action<TEntity, object> GetPropSetter<TEntity>(string propertyName, Type propertyType)
        {
            var obj = Expression.Parameter(typeof(TEntity), propertyName);
            var value = Expression.Parameter(typeof(object));
            var propExp = Expression.Property(obj, propertyName);
            var assignExp = Expression.Assign(propExp, Expression.Convert(value, propertyType));
            return Expression.Lambda<Action<TEntity, object>>(assignExp, obj, value).Compile();
        }

        public static Action<TEntity, object> GetPropSetter<TEntity>(Expression<Func<TEntity, object>> property)
        {
            var prop = GetProperty(property);
            var obj = Expression.Parameter(typeof(TEntity), prop.Name);
            var value = Expression.Parameter(typeof(object));
            var propExp = Expression.Property(obj, prop.Name);
            var assignExp = Expression.Assign(propExp, Expression.Convert(value, prop.PropertyType));
            return Expression.Lambda<Action<TEntity, object>>(assignExp, obj, value).Compile();
        }

        public static PropertyInfo GetProperty<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> expression)
        {
            var member = GetMemberExpression(expression).Member;
            var property = member as PropertyInfo;
            if (property == null)
            {
                throw new InvalidOperationException(string.Format("Member with Name '{0}' is not a property.", member.Name));
            }
            return property;
        }

        private static MemberExpression GetMemberExpression<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> expression)
        {
            MemberExpression memberExpression = null;

            if (expression.Body.NodeType == ExpressionType.Convert)
            {
                var body = (UnaryExpression)expression.Body;
                memberExpression = body.Operand as MemberExpression;
            }
            else if (expression.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpression = expression.Body as MemberExpression;
            }

            if (memberExpression == null)
            {
                throw new ArgumentException("Not a member access", "expression");
            }

            return memberExpression;
        }

        public static Func<TEntity> CreateDefaultConstructor<TEntity>()
        {
            var body = Expression.New(typeof(TEntity));
            var lambda = Expression.Lambda<Func<TEntity>>(body);

            return lambda.Compile();
        }
    }
}
