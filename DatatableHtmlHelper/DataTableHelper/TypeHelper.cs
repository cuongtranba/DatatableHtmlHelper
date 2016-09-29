using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace DatatableHtmlHelper.DataTableHelper
{
    /// <summary>
    /// http://stackoverflow.com/questions/2483023/how-to-test-if-a-type-is-anonymous
    /// </summary>
    public static class TypeHelper
    {
        public static bool CheckIfAnonymousType(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            return Attribute.IsDefined(type, typeof(CompilerGeneratedAttribute), false)
                && type.IsGenericType && type.Name.Contains("AnonymousType")
                && (type.Name.StartsWith("<>") || type.Name.StartsWith("VB$"))
                && (type.Attributes & TypeAttributes.NotPublic) == TypeAttributes.NotPublic;
        }
    }
}