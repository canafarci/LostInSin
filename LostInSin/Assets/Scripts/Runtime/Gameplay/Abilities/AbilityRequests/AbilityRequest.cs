using Sirenix.OdinInspector;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityRequests
{
	public abstract class AbilityRequest : SerializedScriptableObject
	{
		public AbilityRequestType AbilityRequestType;
		public AbilityRequestConfig AbilityRequestConfig;
		public AbilityRequestState abilityRequestState { get; set; }
		public AbilityRequestData abilityRequestData { get; private set; }


		public virtual void Initialize(AbilityRequestData requestData)
		{
			abilityRequestState = AbilityRequestState.Initializing;
			abilityRequestData = requestData;
		}

		// Logic to execute when the action starts
		public abstract void StartRequest();

		// Logic to execute during action execution (if any)
		public abstract void UpdateRequest();
	}
}