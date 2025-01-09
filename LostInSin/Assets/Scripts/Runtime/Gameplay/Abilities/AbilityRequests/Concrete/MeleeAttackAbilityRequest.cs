using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityRequests.Concrete
{
	[CreateAssetMenu(fileName = "Melee Attack Ability Request",
	                 menuName = "LostInSin/Abilities/AbilityRequests/Melee Attack Ability")]
	public class MeleeAttackAbilityRequest : AbilityRequest
	{
		protected override void StartRequest()
		{
			//TODO change cursor
			state = AbilityRequestState.Continue;
		}

		public override void UpdateRequest()
		{
			if (data.PathCells != null && data.TargetCharacter != null)
			{
				// traversed cell count is point count - 1 and one less for the cell which the target is in
				data.DynamicActionPointCost = data.PathCells.Count - 2;
				state = AbilityRequestState.Complete;
			}
		}
	}
}