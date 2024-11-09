using LostInSin.Runtime.Raycast.Data;
using Unity.Collections;
using UnityEngine;

namespace LostInSin.Runtime.Raycast
{
    public interface IGridRaycaster
    {
        public NativeArray<RaycastHit> PerformRaycasting(GridRaycastData gridRaycastData);
    }
}