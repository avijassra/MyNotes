namespace MvcBase.WebHelper
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Globalization;
    using MvcBase.WebHelper;
    using System.Reflection;

    public static class ExpressionExtensions
    {
        /// <summary>
        /// Casts the expression to a lambda expression, removing 
        /// a cast if there's any.
        /// </summary>
        public static LambdaExpression ToLambda(this Expression expression)
        {
            Guard.ArgumentNotNull(expression, "expression");

            LambdaExpression lambda = expression as LambdaExpression;
            if (lambda == null)
                throw new ArgumentException("Expression as lambda expression should not be null");

            // Remove convert expressions which are passed-in by the MockProtectedExtensions.
            // They are passed because LambdaExpression constructor checks the type of 
            // the returned values, even if the return type is Object and everything 
            // is able to convert to it. It forces you to be explicit about the conversion.
            var convert = lambda.Body as UnaryExpression;
            if (convert != null && convert.NodeType == ExpressionType.Convert)
                lambda = Expression.Lambda(convert.Operand, lambda.Parameters.ToArray());

            return lambda;
        }

        /// <summary>
        /// Casts the body of the lambda expression to a <see cref="MethodCallExpression"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If the body is not a method call.</exception>
        public static MethodCallExpression ToMethodCall(this LambdaExpression expression)
        {
            Guard.ArgumentNotNull(expression, "expression");

            var methodCall = expression.Body as MethodCallExpression;
            if (methodCall == null)
            {
                throw new ArgumentException("Expression as method should not be null");
            }

            return methodCall;
        }

        /// <summary>
        /// Converts the body of the lambda expression into the <see cref="PropertyInfo"/> referenced by it.
        /// </summary>
        public static PropertyInfo ToPropertyInfo(this LambdaExpression expression)
        {
            var prop = expression.Body as MemberExpression;

            if (prop != null)
            {
                var info = prop.Member as PropertyInfo;
                if (info != null)
                {
                    return info;
                }
            }

            throw new ArgumentException("Expression as property should not be null");
        }

        /// <summary>
        /// Checks whether the body of the lambda expression is a property access.
        /// </summary>
        public static bool IsProperty(this LambdaExpression expression)
        {
            Guard.ArgumentNotNull(expression, "expression");

            return IsProperty(expression.Body);
        }

        /// <summary>
        /// Checks whether the expression is a property access.
        /// </summary>
        public static bool IsProperty(this Expression expression)
        {
            Guard.ArgumentNotNull(expression, "expression");

            var prop = expression as MemberExpression;

            return prop != null && prop.Member is PropertyInfo;
        }

        /// <summary>
        /// Checks whether the body of the lambda expression is a property indexer, which is true 
        /// when the expression is an <see cref="MethodCallExpression"/> whose 
        /// <see cref="MethodCallExpression.Method"/> has <see cref="MethodBase.IsSpecialName"/> 
        /// equal to <see langword="true"/>.
        /// </summary>
        public static bool IsPropertyIndexer(this LambdaExpression expression)
        {
            Guard.ArgumentNotNull(expression, "expression");

            return IsPropertyIndexer(expression.Body);
        }

        /// <summary>
        /// Checks whether the expression is a property indexer, which is true 
        /// when the expression is an <see cref="MethodCallExpression"/> whose 
        /// <see cref="MethodCallExpression.Method"/> has <see cref="MethodBase.IsSpecialName"/> 
        /// equal to <see langword="true"/>.
        /// </summary>
        public static bool IsPropertyIndexer(this Expression expression)
        {
            Guard.ArgumentNotNull(expression, "expression");

            var call = expression as MethodCallExpression;

            return call != null && call.Method.IsSpecialName;
        }

        /// <summary>
        /// Creates an expression that casts the given expression to the <typeparamref name="T"/> 
        /// type.
        /// </summary>
        public static Expression CastTo<T>(this Expression expression)
        {
            return Expression.Convert(expression, typeof(T));
        }
    }
}
