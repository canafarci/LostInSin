using Sirenix.OdinInspector;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityRequests
{
	public abstract class AbilityRequest : SerializedScriptableObject
	{
		public AbilityRequestType AbilityRequestType;
		public AbilityRequestState abilityRequestState { get; set; }

		private AbilityRequestData _abilityRequestData;

		public AbilityRequestData abilityRequestData => _abilityRequestData;

		public virtual void Initialize(AbilityRequestData requestData)
		{
			abilityRequestState = AbilityRequestState.Initializing;
			_abilityRequestData = requestData;
		}

		// Logic to execute when the action starts
		public abstract void StartRequest();

		// Logic to execute during action execution (if any)
		public abstract AbilityRequestState UpdateRequest();
	}
}