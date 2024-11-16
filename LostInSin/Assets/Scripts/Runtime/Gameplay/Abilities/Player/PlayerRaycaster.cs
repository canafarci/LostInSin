using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using LostInSin.Runtime.Grid;
using LostInSin.Runtime.Grid.Data;
using UnityEngine;
using VContainer.Unity;

namespace LostInSin.Runtime.Gameplay.Abilities.Player
{
	public class PlayerRaycaster : IInitializable
	{
		private readonly IGridPositionConverter _gridPositionConverter;
		private Camera _mainCamera;

		public PlayerRaycaster(IGridPositionConverter gridPositionConverter)
		{
			_gridPositionConverter = gridPositionConverter;
		}

		public void Initialize()
		{
			_mainCamera = Camera.main;
		}

		//this is called inside FixedUpdate
		public void TryRaycast(AbilityRequest abilityRequest, ref RaycastRequest raycastRequest)
		{
			Ray ray = _mainCamera.ScreenPointToRay(raycastRequest.mousePosition);
			LayerMask mask = abilityRequest.AbilityRequestConfig.LayerMask;
			AbilityRequestType requestType = abilityRequest.AbilityRequestType;

			if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, mask))
			{
				raycastRequest.isProcessed = true;
				return;
			}

			Vector3 position = hit.point;

			if (requestType.HasFlag(AbilityRequestType.GridPositionRaycasted))
			{
				if (_gridPositionConverter.GetCell(position, out GridCellData gridCellData) && !gridCellData.isOccupied)
				{
					abilityRequest.abilityRequestData.TargetGridCell = gridCellData;
				}
			}
			else if (requestType.HasFlag(AbilityRequestType.PositionRaycasted))
			{
				abilityRequest.abilityRequestData.TargetPosition = position;
			}

			raycastRequest.isProcessed = true;
		}
	}
}