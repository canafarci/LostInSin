using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using LostInSin.Runtime.Gameplay.Grid;
using LostInSin.Runtime.Gameplay.Grid.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer.Unity;
using RaycastHit = UnityEngine.RaycastHit;

namespace LostInSin.Runtime.Gameplay.Abilities.RequestFilling
{
	public class PlayerRaycaster : IInitializable
	{
		private readonly IGridPositionConverter _gridPositionConverter;
		private Camera _mainCamera;

		private RaycastHit[] _hits = new RaycastHit[10];

		public PlayerRaycaster(IGridPositionConverter gridPositionConverter)
		{
			_gridPositionConverter = gridPositionConverter;
		}

		public void Initialize()
		{
			_mainCamera = Camera.main;
		}

		public bool TryRaycastForComponent<T>(out T component)
			where T : MonoBehaviour
		{
			Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
			LayerMask mask = LayerMask.GetMask("Character");


			if (TryRaycast(ray, mask, out RaycastHit hit) &&
			    hit.transform.TryGetComponent(out T hitComponent))
			{
				component = hitComponent;
				return true;
			}

			component = null;
			return false;
		}

		public bool TryRaycastForComponent<T>(ref RaycastRequest raycastRequest, LayerMask layerMask, out T component)
			where T : MonoBehaviour
		{
			Ray ray = _mainCamera.ScreenPointToRay(raycastRequest.mousePosition);

			if (TryRaycast(ray, layerMask, out RaycastHit hit) &&
			    hit.transform.TryGetComponent(out T hitComponent))
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


		private bool IsInvalidRaycast(Ray ray, LayerMask mask, out RaycastHit hit)
		{
			hit = default;
			return EventSystem.current.IsPointerOverGameObject() ||
			       !TryRaycast(ray, mask, out hit);
		}

		private bool TryRaycast(Ray ray, LayerMask mask, out RaycastHit hit)
		{
			hit = default;

			// Perform the raycast and check if there are any hits
			int hitCount = Physics.RaycastNonAlloc(ray, _hits, Mathf.Infinity, mask, QueryTriggerInteraction.Ignore);

			// Loop through all hits and check if any are on the specified layer mask
			for (int i = 0; i < hitCount; i++)
			{
				if ((mask.value & (1 << _hits[i].collider.gameObject.layer)) != 0)
				{
					// Object is in the specified layer mask
					hit = _hits[i];
					return true;
				}
			}

			// No object in the specified layer mask was hit
			return false;
		}
	}
}