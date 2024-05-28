using System;
using JetBrains.Annotations;
using VContainer;

namespace RpDev.Services.GenericFactories.VContainer
{
    [UsedImplicitly]
    public class PlainClassFactory : IPlainClassFactory
    {
        private readonly IObjectResolver _resolver;
        
        public PlainClassFactory(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        public T Create<T>(params object[] extraArgs) where T : class
        {
            var type = typeof(T);

            if (type.IsAbstract)
                throw new ArgumentException($"Abstract class '{type}' cannot be instantiated.");

            if (type.IsInterface)
                throw new ArgumentException($"Interface '{type}' cannot be instantiated.");
            
            return _resolver?.CreateInstance<T>();
        }

        public object Create(Type type, params object[] extraArgs)
        {
            if (type.IsAbstract)
                throw new ArgumentException($"Abstract class '{type}' cannot be instantiated.");

            if (type.IsInterface)
                throw new ArgumentException($"Interface '{type}' cannot be instantiated.");

            return _resolver?.CreateInstance(type);
        }
    }
}