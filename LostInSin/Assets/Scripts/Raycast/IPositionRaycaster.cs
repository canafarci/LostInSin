using UnityEngine;

namespace LostInSin.Raycast
{
    public interface IPositionRaycaster
    {
        public bool GetWorldPosition(out Vector3 position);
    }
}