using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using LostInSin.Runtime.Gameplay.Characters;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities.RequestFilling.RequestHandlers
{
	public class EnemyTargetedHandler : AbilityRequestTypeHandlerBase
	{
		public override bool AppliesTo(AbilityRequestType requestType)
		{
			return requestType.HasFlag(AbilityRequestType.EnemyTargeted);
		}

		protected override void ProcessRequest(AbilityRequest abilityRequest, PlayerAbilityRequestFiller context)
		{
			// Example: do a raycast for enemy or validate the targeted enemy
			// You can reuse your _playerRaycaster from context
			if (context.RaycastRequest != null && !context.RaycastRequest.isProcessed)
			{
				if (context.playerRaycaster.TryRaycastForComponent(
				                                                   ref context.RaycastRequest,
				                                                   abilityRequest.CharacterLayerMask,
				                                                   out CharacterFacade character)
				    && !character.isPlayerCharacter)
				{
					abilityRequest.data.TargetCharacter = character;
				}
			}
		}
	}
}