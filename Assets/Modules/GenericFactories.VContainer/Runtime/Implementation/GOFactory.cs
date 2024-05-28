using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace RpDev.Services.GenericFactories.VContainer
{
    public class GOFactory : IGOFactory
    {
        private readonly IObjectResolver _resolver;

        public GOFactory(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        public T Create<T>(T prototype) where T : Component
        {
            return _resolver.Instantiate(prototype);
        }
        
        public T Create<T>(T prototype, Transform parent) where T : Component
        {
            return _resolver.Instantiate(prototype, parent);
        }
        
        public T Create<T>(T prototype, Transform parent, Vector3 position) where T : Component
        {
            return _resolver.Instantiate(prototype, position, Quaternion.identity, parent);
        }
        
        public T Create<T>(T prototype, Transform parent, Vector3 position, Quaternion quaternion) where T : Component
        {
            return _resolver.Instantiate(prototype, position, quaternion, parent);
        }
    }
}