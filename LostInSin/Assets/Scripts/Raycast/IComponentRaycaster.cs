
using UnityEngine;

namespace LostInSin.Raycast
{
    public interface IComponentRaycaster<T> where T : Component
    {
        public bool RaycastComponent(out T characterTransform, int layerMask);
    }
}