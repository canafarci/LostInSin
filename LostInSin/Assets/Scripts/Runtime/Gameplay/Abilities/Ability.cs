using LostInSin.Runtime.Gameplay.Abilities.AbilityExecution;
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
		public int DefaultActionPointCost;
		public int DynamicActionPointCost;
		public Sprite Icon;
		public AbilityRequest AbilityRequest;
		public AbilityExecutionLogic AbilityExecutionLogic;
	}
}