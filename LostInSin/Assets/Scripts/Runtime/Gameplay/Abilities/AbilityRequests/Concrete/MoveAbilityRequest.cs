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
			state = AbilityRequestState.Continue;
		}

		public override void UpdateRequest()
		{
			if (data.PathCells != null)
			{
				data.User.SetCharacterCell(data.TargetGridCell);
				state = AbilityRequestState.Complete;
			}
		}
	}
}