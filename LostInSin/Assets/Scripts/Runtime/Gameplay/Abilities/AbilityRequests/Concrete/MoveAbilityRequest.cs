using LostInSin.Runtime.Grid.Data;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityRequests.Concrete
{
	[CreateAssetMenu(fileName = "Move Ability Request",
	                 menuName = "LostInSin/Abilities/AbilityRequests/Move Ability")]
	public class MoveAbilityRequest : AbilityRequest
	{
		protected override void StartRequest()
		{
			//TODO change cursor
			state = AbilityRequestState.Continue;
		}

		public override void UpdateRequest()
		{
			if (data.PathCells != null)
			{
				data.DynamicActionPointCost = data.PathCells.Count - 1;
				state = AbilityRequestState.Complete;
			}
		}
	}
}