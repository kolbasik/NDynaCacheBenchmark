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

        public static T CreateInstance(MemoryCacheService memoryCacheService, params object[] arguments)
        {
            if (memoryCacheService == null) throw new ArgumentNullException(nameof(memoryCacheService));
            var objects = new object[] {memoryCacheService}.Concat(arguments).ToArray();
            return (T) System.Activator.CreateInstance(CacheableType, objects);
        }
    }
}