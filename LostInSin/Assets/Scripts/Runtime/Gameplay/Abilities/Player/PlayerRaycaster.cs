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

		public bool TryRaycastForEmptyGridCell(AbilityRequest abilityRequest, ref RaycastRequest raycastRequest, out GridCell cell)
		{
			if (TryRaycastForGridCell(abilityRequest, ref raycastRequest, out cell) && !cell.isOccupied)
				return true;
			else
				return false;
		}


		//this is called inside FixedUpdate
		public bool TryRaycastForGridCell(AbilityRequest abilityRequest, ref RaycastRequest raycastRequest, out GridCell cell)
		{
			raycastRequest.isProcessed = true;

			if (TryRaycastForPosition(abilityRequest, ref raycastRequest, out Vector3 position) &&
			    _gridPositionConverter.GetCell(position, out GridCell gridCell))
			{
				cell = gridCell;
				return true;
			}
			else
			{
				cell = null;
				return false;
			}
		}

		public bool TryRaycastForPosition(AbilityRequest abilityRequest,
			ref RaycastRequest raycastRequest,
			out Vector3 position)
		{
			raycastRequest.isProcessed = true;

			Ray ray = _mainCamera.ScreenPointToRay(raycastRequest.mousePosition);
			LayerMask mask = abilityRequest.Config.LayerMask;
			AbilityRequestType requestType = abilityRequest.RequestType;
			position = default;

			if (IsInvalidRaycast(ray, mask, out RaycastHit hit))
			{
				return false;
			}

			position = hit.point;
			return true;
		}

		//this is called inside FixedUpdate
		public bool TryRaycastForGridCell(out GridCell gridCell)
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