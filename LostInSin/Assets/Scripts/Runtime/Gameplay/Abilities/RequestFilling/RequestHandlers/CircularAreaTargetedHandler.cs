using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using LostInSin.Runtime.Gameplay.Grid;
using LostInSin.Runtime.Gameplay.Grid.Data;
using VContainer;

namespace LostInSin.Runtime.Gameplay.Abilities.RequestFilling.RequestHandlers
{
	public class CircularAreaTargetedHandler : AbilityRequestTypeHandlerBase
	{
		[Inject] private IGridPositionConverter _converter;

		public override bool AppliesTo(AbilityRequestType requestType)
		{
			return requestType.HasFlag(AbilityRequestType.CircularAreaTargeted);
		}

		protected override void ProcessRequest(AbilityRequest abilityRequest)
		{
			if (abilityRequest.data.TargetGridCell == null) return;

			List<GridCell> cellsWithinRadius = _converter.GetCellsWithinRadius(abilityRequest.data.TargetGridCell, abilityRequest.Radius);
			abilityRequest.data.TargetGridCells = cellsWithinRadius;
		}
	}
}