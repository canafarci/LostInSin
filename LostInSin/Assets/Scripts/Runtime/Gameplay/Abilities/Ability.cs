using LostInSin.Runtime.Gameplay.Abilities.AbilityExecutions;
using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace LostInSin.Runtime.Gameplay.Abilities
{
	[CreateAssetMenu(fileName = "Ability",
	                 menuName = "LostInSin/Abilities/Ability")]
	public class Ability : SerializedScriptableObject
	{
		public string AbilityName;
		public Sprite Icon;
		public AbilityRequest AbilityRequest;
		public AbilityExecution AbilityExecution;
	}
}