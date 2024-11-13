using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using Sirenix.OdinInspector;

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
		public abstract void StartAction();

		// Logic to execute during action execution (if any)
		public abstract void UpdateAction();

		// Finalize action
		protected virtual void EndAction()
		{
			executionStage = AbilityExecutionStage.Complete;
		}
	}
}