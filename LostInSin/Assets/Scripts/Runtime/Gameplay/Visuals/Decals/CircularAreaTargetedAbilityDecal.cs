using LostInSin.Runtime.Infrastructure.MemoryPool;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Visuals.Decals
{
	public class CircularAreaTargetedAbilityDecal : MonoPoolable
	{
		public override void OnReturnToPool()
		{
			transform.localScale = Vector3.one;
		}
	}
}