using Sirenix.OdinInspector;
using UnityEngine.Serialization;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityRequests
{
	public abstract class AbilityRequest : SerializedScriptableObject
	{
		[EnumToggleButtons] public AbilityRequestType RequestType;
		public AbilityRequestConfig Config;
		public AbilityRequestState state { get; set; }
		public AbilityRequestData data { get; private set; }


		public virtual void Initialize(AbilityRequestData requestData)
		{
			state = AbilityRequestState.Initializing;
			data = requestData;
		}

		// Logic to execute when the action starts
		public abstract void StartRequest();

		// Logic to execute during action execution (if any)
		public abstract void UpdateRequest();
	}
}