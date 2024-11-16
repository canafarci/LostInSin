using Sirenix.OdinInspector;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityRequests
{
	[CreateAssetMenu(fileName = "Ability Request Config",
		menuName = "LostInSin/Abilities/AbilityRequests/Ability Request Config", order = 0)]
	public class AbilityRequestConfig : SerializedScriptableObject
	{
		public LayerMask LayerMask;
	}
}