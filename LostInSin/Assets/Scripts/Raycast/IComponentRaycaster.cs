
using UnityEngine;

namespace LostInSin.Raycast
{
    public interface IComponentRaycaster<T> where T : Component
    {
        public bool RaycastCharacter(out T characterTransform, int layerMask);
    }
}