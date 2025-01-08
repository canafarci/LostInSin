using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using LostInSin.Runtime.Gameplay.Grid;
using LostInSin.Runtime.Gameplay.Grid.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer.Unity;

namespace LostInSin.Runtime.Gameplay.Abilities.RequestFilling
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

		public bool TryRaycastForComponent<T>(ref RaycastRequest raycastRequest, LayerMask layerMask, out T component)
			where T : MonoBehaviour
		{
			Ray ray = _mainCamera.ScreenPointToRay(raycastRequest.mousePosition);

			if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, layerMask) &&
			    raycastHit.transform.TryGetComponent(out T hitComponent))
			{
				component = hitComponent;
				return true;
			}

			component = null;
			return false;
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
			Ray ray = _mainCamera.ScreenPointToRay(raycastRequest.mousePosition);
			LayerMask mask = abilityRequest.GroundLayerMask;
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