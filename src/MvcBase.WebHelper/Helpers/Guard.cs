namespace MvcBase.WebHelper
{
    using System;
    using System.Globalization;

    internal static class Guard
    {
        /// <summary>
        /// Checks an argument to ensure it isn't null.
        /// </summary>
        /// <param name="value">The argument value to check.</param>
        /// <param name="argumentName">The name of the argument.</param>
        public static void ArgumentNotNull(object value, string argumentName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        /// <summary>
        /// Checks a string argument to ensure it isn't null or empty.
        /// </summary>
        /// <param name="argumentValue">The argument value to check.</param>
        /// <param name="argumentName">The name of the argument.</param>
        public static void ArgumentNotNullOrEmptyString(string argumentValue, string argumentName)
        {
            ArgumentNotNull(argumentValue, argumentName);

            if (argumentValue.Length == 0)
            {
                throw new ArgumentException(string.Format("{0} should not be null", argumentName));
            }
        }


        /// <summary>
        /// Checks an argument to ensure it is in the specified range including the edges.
        /// </summary>
        /// <typeparam name="T">Type of the argument to check, it must be an <see cref="IComparable"/> type.
        /// </typeparam>
        /// <param name="value">The argument value to check.</param>
        /// <param name="from">The minimun allowed value for the argument.</param>
        /// <param name="to">The maximun allowed value for the argument.</param>
        /// <param name="argumentName">The name of the argument.</param>
        public static void ArgumentNotOutOfRangeInclusive<T>(T value, T from, T to, string argumentName)
                where T : IComparable
        {
            if (value != null && (value.CompareTo(from) < 0 || value.CompareTo(to) > 0))
            {
                throw new ArgumentOutOfRangeException(argumentName);
            }
        }

        /// <summary>
        /// Checks an argument to ensure it is in the specified range excluding the edges.
        /// </summary>
        /// <typeparam name="T">Type of the argument to check, it must be an <see cref="IComparable"/> type.
        /// </typeparam>
        /// <param name="value">The argument value to check.</param>
        /// <param name="from">The minimun allowed value for the argument.</param>
        /// <param name="to">The maximun allowed value for the argument.</param>
        /// <param name="argumentName">The name of the argument.</param>
        public static void ArgumentNotOutOfRangeExclusive<T>(T value, T from, T to, string argumentName)
                where T : IComparable
        {
            if (value != null && (value.CompareTo(from) <= 0 || value.CompareTo(to) >= 0))
            {
                throw new ArgumentOutOfRangeException(argumentName);
            }
        }

        public static void CanBeAssigned(Type typeToAssign, Type targetType, string argumentName)
        {
            if (!targetType.IsAssignableFrom(typeToAssign))
            {
                if (targetType.IsInterface)
                {
                    throw new ArgumentException(string.Format("{0} should not be null", argumentName));
                }

                throw new ArgumentException(string.Format("{0} should not be null", argumentName));
            }
        }
    }
}
