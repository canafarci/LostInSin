using Raycast.Data;
using Unity.Collections;
using UnityEngine;

namespace LostInSin.Raycast
{
    public interface IGridRaycaster
    {
        public NativeArray<RaycastHit> PerformRaycasting(GridRaycastData gridRaycastData);
    }
}
