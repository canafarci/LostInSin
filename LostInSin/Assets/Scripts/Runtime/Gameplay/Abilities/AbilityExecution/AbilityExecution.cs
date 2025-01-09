using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using LostInSin.Runtime.Infrastructure.MemoryPool;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityExecution
{
	public abstract class AbilityExecution : SerializedScriptableObject
	{
		public AbilityExecutionStage executionStage { get; protected set; }

		public AbilityRequestData requestData { get; private set; }
		public AbilityExecutionData executionData { get; private set; }

		public virtual void Initialize(AbilityRequestData requestData)
		{
			this.requestData = requestData;
			executionStage = AbilityExecutionStage.Starting;
			executionData = PoolManager.GetPure<AbilityExecutionData>();
		}

		// Logic to execute when the action starts
		public abstract void StartAbility();

		// Logic to execute during action execution (if any)
		public abstract void UpdateAbility();

		// Finalize action (if any)
		public virtual void FinishAbility()
		{
		}

		// Clean up resources (if any)
		public virtual void EndAbility()
		{
			Debug.Log($"{name} completed");
			PoolManager.ReleasePure(executionData);
		}
	}
}