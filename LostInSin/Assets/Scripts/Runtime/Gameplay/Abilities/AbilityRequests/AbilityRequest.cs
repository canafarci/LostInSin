using LostInSin.Runtime.Infrastructure.MemoryPool;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityRequests
{
	public abstract class AbilityRequest : SerializedScriptableObject
	{
		[EnumToggleButtons] public AbilityRequestType RequestType;
		public AbilityRequestState state { get; set; }
		public AbilityRequestData data { get; private set; }

		public int DefaultActionPointCost;

		[ShowIf("@this.RequestType.HasFlag(AbilityRequestType.PositionRaycasted) || this.RequestType.HasFlag(AbilityRequestType.GridPositionRaycasted)")]
		public LayerMask GroundLayerMask;

		[ShowIf("@this.RequestType.HasFlag(AbilityRequestType.EnemyTargeted)")]
		public LayerMask CharacterLayerMask;

		[ShowIf("@this.RequestType.HasFlag(AbilityRequestType.CircularAreaTargeted)")]
		public float Radius;

		public virtual void Initialize()
		{
			state = AbilityRequestState.Initializing;
			data = PoolManager.GetPure<AbilityRequestData>();
			data.DefaultActionPointCost = DefaultActionPointCost;
			StartRequest();
		}

		// Logic to execute when the action starts
		protected abstract void StartRequest();

		// Logic to execute during action execution (if any)
		public abstract void UpdateRequest();
	}
}