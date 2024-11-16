using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using LostInSin.Runtime.Grid;
using LostInSin.Runtime.Grid.Data;
using UnityEngine;
using UnityEngine.EventSystems;
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
			LayerMask mask = abilityRequest.Config.LayerMask;
			AbilityRequestType requestType = abilityRequest.RequestType;

			if (IsInvalidRaycast(ray, mask, out RaycastHit hit))
			{
				raycastRequest.isProcessed = true;
				return;
			}

			Vector3 position = hit.point;

			if (requestType.HasFlag(AbilityRequestType.GridPositionRaycasted))
			{
				if (_gridPositionConverter.GetCell(position, out GridCell gridCell) && !gridCell.isOccupied)
				{
					abilityRequest.data.TargetGridCell = gridCell;
				}
			}
			else if (requestType.HasFlag(AbilityRequestType.PositionRaycasted))
			{
				abilityRequest.data.TargetPosition = position;
			}

			raycastRequest.isProcessed = true;
		}

		//this is called inside FixedUpdate
		public bool RaycastForGridCell(out GridCell gridCell)
		{
			Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
			LayerMask mask = LayerMask.GetMask("Ground");
			gridCell = null;

			if (IsInvalidRaycast(ray, mask, out RaycastHit hit)) return false;

			if (_gridPositionConverter.GetCell(hit.point, out GridCell newGridCell))
			{
				gridCell = newGridCell;
				return true;
			}

			return false;
		}


		private static bool IsInvalidRaycast(Ray ray, LayerMask mask, out RaycastHit hit)
		{
			hit = default;
			return EventSystem.current.IsPointerOverGameObject() ||
			       !Physics.Raycast(ray, out hit, Mathf.Infinity, mask);
		}
	}
}