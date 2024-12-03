using LostInSin.Runtime.Gameplay.Raycast.Data;
using Unity.Collections;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Raycast
{
	public interface IGridRaycaster
	{
		public NativeArray<RaycastHit> PerformRaycasting(GridRaycastData gridRaycastData);
	}
}