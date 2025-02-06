using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using LostInSin.Runtime.Gameplay.Characters;
using LostInSin.Runtime.Gameplay.Characters.Enums;
using UnityEngine;
using VContainer;

namespace LostInSin.Runtime.Gameplay.Abilities.RequestFilling.RequestHandlers
{
	public class EnemyTargetedHandler : AbilityRequestTypeHandlerBase
	{
		[Inject] private PlayerAbilityRequestFiller _filler;
		[Inject] private PlayerRaycaster _raycaster;

		public override bool AppliesTo(AbilityRequestType requestType)
		{
			return requestType.HasFlag(AbilityRequestType.EnemyTargeted);
		}

		protected override void ProcessRequest(AbilityRequest abilityRequest)
		{
			if (_filler.RaycastRequest != null && !_filler.RaycastRequest.isProcessed)
			{
				if (_raycaster.TryRaycastForComponent(ref _filler.RaycastRequest,
				                                      abilityRequest.CharacterLayerMask,
				                                      out CharacterFacade character)
				    && character.teamID != TeamID.Player)
				{
					abilityRequest.data.TargetCharacter = character;
				}
			}
		}
	}
}