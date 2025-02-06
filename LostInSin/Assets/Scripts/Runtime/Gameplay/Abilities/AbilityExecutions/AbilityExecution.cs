using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using LostInSin.Runtime.Infrastructure.MemoryPool;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityExecutions
{
	public abstract class AbilityExecution : SerializedScriptableObject
	{
		public AbilityExecutionStage executionStage { get; protected set; }

		public AbilityExecutionData executionData { get; private set; }

		public virtual void Initialize(AbilityRequestData data)
		{
			Debug.Log($"{name} execution started");

			executionStage = AbilityExecutionStage.Starting;
			executionData = PoolManager.GetPure<AbilityExecutionData>();
			executionData.User = data.User;
		}

		// Logic to execute when the action starts
		public abstract void StartAbility();

		// Logic to execute during action execution (if any)
		public abstract void UpdateAbility();

		// Finalize action (if any)
		public abstract void FinishAbility();

		// Clean up resources (if any)
		public virtual void EndAbility()
		{
			Debug.Log($"{name} execution completed");
			PoolManager.ReleasePure(executionData);
		}
	}
}