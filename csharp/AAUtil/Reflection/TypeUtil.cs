using System.Linq;

namespace System.Reflection
{
    /// <summary>
    /// 
    /// </summary>
    public static class TypeUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="child"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static bool InheritsOrImplements(this Type child, Type parent)
        {
            var currentChild = child;

            while (currentChild != null && currentChild != typeof(object))
            {
                if (currentChild.IsGenericType)
                {
                    currentChild = currentChild.GetGenericTypeDefinition();
                }

                if (parent == currentChild || HasAnyInterfaces(parent, currentChild))
                {
                    return true;
                }

                currentChild = currentChild.BaseType;
            }

            return false;
        }

        private static bool HasAnyInterfaces(Type parent, Type child)
        {
            return child.GetInterfaces().Any(childInterface =>
            {
                var currentInterface = childInterface.IsGenericType
                    ? childInterface.GetGenericTypeDefinition()
                    : childInterface;

                return currentInterface == parent;
            });
        }
    }
}
