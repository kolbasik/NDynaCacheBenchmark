using System;
using System.Linq;

namespace DynaCache
{
    public static class CacheableActivator<T>
    {
        private static readonly Type CacheableType;

        static CacheableActivator()
        {
            CacheableType = Cacheable.CreateType<T>();
        }

        public static T CreateInstance(params object[] arguments)
        {
            return CreateInstance(new MemoryCacheService(), arguments);
        }

        public static T CreateInstance(IDynaCacheService dynaCacheService, params object[] arguments)
        {
            if (dynaCacheService == null) throw new ArgumentNullException(nameof(dynaCacheService));
            var objects = new object[] {dynaCacheService}.Concat(arguments).ToArray();
            return (T) Activator.CreateInstance(CacheableType, objects);
        }
    }
}