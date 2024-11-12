using LostInSin.Runtime.Gameplay.Abilities.AbilityExecution;
using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities
{
	[CreateAssetMenu(fileName = "Ability",
		menuName = "LostInSin/Abilities/Ability")]
	public class Ability : SerializedScriptableObject
	{
		public string AbilityName;
		public int ActionPointCost;
		public Sprite Icon;
		public AbilityRequest AbilityRequest;
		public AbilityExecutionLogic AbilityExecutionLogic;
	}
}