using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;

namespace LostInSin.Runtime.Gameplay.Abilities.RequestFilling.RequestHandlers
{
	public class EnemyTargetedPathfindingHandler : AbilityRequestTypeHandlerBase
	{
		public override bool AppliesTo(AbilityRequestType requestType)
		{
			return requestType.HasFlag(AbilityRequestType.GridPathFinding) &&
			       requestType.HasFlag(AbilityRequestType.EnemyTargeted);
		}

		protected override void ProcessRequest(AbilityRequest abilityRequest)
		{
			if (abilityRequest.data.TargetCharacter == null) return;

			abilityRequest.data.TargetGridCell = abilityRequest.data.TargetCharacter.currentCell;
		}
	}
}