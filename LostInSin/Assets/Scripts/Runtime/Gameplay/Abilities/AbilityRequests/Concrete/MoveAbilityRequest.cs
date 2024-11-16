using LostInSin.Runtime.Grid.Data;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityRequests.Concrete
{
	[CreateAssetMenu(fileName = "Move Ability Request",
		menuName = "LostInSin/Abilities/AbilityRequests/Move Ability")]
	public class MoveAbilityRequest : AbilityRequest
	{
		public override void StartRequest()
		{
			//TODO change cursor
			abilityRequestState = AbilityRequestState.Continue;
		}

		public override void UpdateRequest()
		{
			if (abilityRequestData.TargetGridCell != default(GridCellData))
			{
				abilityRequestData.TargetGridCell.SetAsOccupied();
				abilityRequestState = AbilityRequestState.Complete;
			}
		}
	}
}