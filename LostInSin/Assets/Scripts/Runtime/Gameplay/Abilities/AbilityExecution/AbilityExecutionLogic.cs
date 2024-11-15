using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityExecution
{
	public abstract class AbilityExecutionLogic : SerializedScriptableObject
	{
		protected AbilityRequestData _abilityRequestData;

		public AbilityExecutionStage executionStage { get; set; }

		public AbilityRequestData abilityRequestData => _abilityRequestData;

		public virtual void Initialize(AbilityRequestData requestData)
		{
			_abilityRequestData = requestData;
			executionStage = AbilityExecutionStage.Starting;
		}

		// Logic to execute when the action starts
		public abstract void StartAbility();

		// Logic to execute during action execution (if any)
		public abstract void UpdateAbility();

		// Finalize action
		protected virtual void EndAbility()
		{
			executionStage = AbilityExecutionStage.Complete;
			Debug.Log("Action Ended");
		}
	}
}