using System;
using System.Reflection;

namespace ByteDev.Ioc.MsExtDi
{
    internal static class TypeExtensions
    {
        public static bool IsInstaller(this Type type)
        {
            var typeInfo = type.GetTypeInfo();
            return typeof(IServiceInstaller).GetTypeInfo().IsAssignableFrom(typeInfo) && typeInfo.IsClass && !typeInfo.IsAbstract;
        }
    }
}