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
			if (IsRequestDataValid())
			{
				//remove the cost of the cell the character starts the movement at
				data.DynamicActionPointCost = data.PathCells.Count - 1;
				state = AbilityRequestState.Complete;
			}
		}

		private bool IsRequestDataValid()
		{
			return data.PathCells != null &&
			       data.TargetCharacter != null
			       && data.TargetCharacter.isDead == false;
		}
	}
}