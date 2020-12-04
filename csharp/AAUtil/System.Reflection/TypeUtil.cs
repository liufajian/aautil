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

        ///////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        /// <param name="child"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static bool IsSubClassOfGeneric(this Type child, Type parent)
        {
            if (child == parent)
            {
                return false;
            }

            if (child.IsSubclassOf(parent))
            {
                return true;
            }

            var parameters = parent.GetGenericArguments();
            var isParameterLessGeneric = !(parameters != null && parameters.Length > 0 &&
                ((parameters[0].Attributes & TypeAttributes.BeforeFieldInit) == TypeAttributes.BeforeFieldInit));

            while (child != null && child != typeof(object))
            {
                var cur = GetFullTypeDefinition(child);

                if (parent == cur || (isParameterLessGeneric && cur.GetInterfaces().Select(i => GetFullTypeDefinition(i)).Contains(GetFullTypeDefinition(parent))))
                {
                    return true;
                }
                else if (!isParameterLessGeneric)
                {
                    if (GetFullTypeDefinition(parent) == cur && !cur.IsInterface)
                    {
                        if (VerifyGenericArguments(GetFullTypeDefinition(parent), cur) && VerifyGenericArguments(parent, child))
                        {
                            return true;
                        }
                    }
                    else
                    {
                        foreach (var item in child.GetInterfaces().Where(i => GetFullTypeDefinition(parent) == GetFullTypeDefinition(i)))
                        {
                            if (VerifyGenericArguments(parent, item))
                            {
                                return true;
                            }
                        }
                    }
                }

                child = child.BaseType;
            }

            return false;
        }

        private static Type GetFullTypeDefinition(Type type)
        {
            return type.IsGenericType ? type.GetGenericTypeDefinition() : type;
        }

        private static bool VerifyGenericArguments(Type parent, Type child)
        {
            var childArguments = child.GetGenericArguments();
            var parentArguments = parent.GetGenericArguments();
            if (childArguments.Length == parentArguments.Length)
            {
                for (var i = 0; i < childArguments.Length; i++)
                {
                    if (childArguments[i].Assembly != parentArguments[i].Assembly || childArguments[i].Name != parentArguments[i].Name || childArguments[i].Namespace != parentArguments[i].Namespace)
                    {
                        if (!childArguments[i].IsSubclassOf(parentArguments[i]))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}
