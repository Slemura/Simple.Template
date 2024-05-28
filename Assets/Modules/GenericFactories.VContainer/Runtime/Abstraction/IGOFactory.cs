using UnityEngine;

namespace RpDev.Services.GenericFactories.VContainer
{
    public interface IGOFactory
    {
        T Create<T>(T prototype) where T : Component;
        T Create<T>(T prototype, Transform parent) where T : Component;
        T Create<T>(T prototype, Transform parent, Vector3 position) where T : Component;
        T Create<T>(T prototype, Transform parent, Vector3 position, Quaternion quaternion) where T : Component;
    }
}