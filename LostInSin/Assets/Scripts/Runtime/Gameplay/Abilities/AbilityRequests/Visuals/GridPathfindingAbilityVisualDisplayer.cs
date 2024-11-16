using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Abilities.Player;
using LostInSin.Runtime.Gameplay.Signals;
using LostInSin.Runtime.Grid.Data;
using LostInSin.Runtime.Infrastructure.Templates;
using LostInSin.Runtime.Pathfinding;
using LostInSin.Runtime.Raycast;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer.Unity;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityRequests.Visuals
{
	public class GridPathfindingAbilityVisualDisplayer : SignalListener, IFixedTickable
	{
		private AbilityRequest _abilityRequest;
		private readonly IGridPathfinder _gridPathfinder;
		private readonly PlayerRaycaster _playerRaycaster;
		private readonly LineRenderer _lineRenderer;
		private const float UPDATE_INTERVAL = 0.05f;
		private float _updateTimer = 0f;

		public GridPathfindingAbilityVisualDisplayer(IGridPathfinder gridPathfinder, PlayerRaycaster playerRaycaster)
		{
			_gridPathfinder = gridPathfinder;
			_playerRaycaster = playerRaycaster;
			_lineRenderer = new GameObject("LineRenderer").AddComponent<LineRenderer>();

			_lineRenderer.startWidth = 0.1f;
			_lineRenderer.endWidth = 0.1f;
			_lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
			_lineRenderer.startColor = Color.green;
			_lineRenderer.endColor = Color.green;
		}

		protected override void SubscribeToEvents()
		{
			_signalBus.Subscribe<AbilityRequestCreatedSignal>(OnAbilityRequestCreatedSignal);
		}

		private void OnAbilityRequestCreatedSignal(AbilityRequestCreatedSignal signal)
		{
			AbilityRequest abilityRequest = signal.request;
			if (!abilityRequest.data.User.isPlayerCharacter) return;
			if (!abilityRequest.RequestType.HasFlag(AbilityRequestType.GridPathFinding)) return;

			_abilityRequest = abilityRequest;
		}

		public void FixedTick()
		{
			if (ShouldNotDraw()) return;

			_playerRaycaster.RaycastForGridCell(out GridCell targetCell);

			DisplayPathAsLine(targetCell);
		}

		private bool ShouldNotDraw()
		{
			if (_abilityRequest == null) return true;
			if (_abilityRequest.state == AbilityRequestState.Complete)
			{
				ClearLineRenderer();
				return true;
			}

			if (EventSystem.current.IsPointerOverGameObject()) return true;
			if (ShouldNotUpdate()) return true;
			return false;
		}

		private void DisplayPathAsLine(GridCell targetCell)
		{
			if (_gridPathfinder.FindPath(_abilityRequest.data.User.currentCell, targetCell, out List<GridCell> path))
			{
				// Convert the path of GridCells to an array of Vector3 positions
				Vector3[] positions = new Vector3[path.Count];
				for (int i = 0; i < path.Count; i++)
				{
					positions[i] = path[i].centerPosition + Vector3.up * 0.15f;
				}

				// Configure the LineRenderer
				_lineRenderer.positionCount = positions.Length;
				_lineRenderer.SetPositions(positions);
			}
			else
			{
				ClearLineRenderer();
			}
		}

		private void ClearLineRenderer()
		{
			// No path found, clear the LineRenderer
			_lineRenderer.positionCount = 0;
		}

		private bool ShouldNotUpdate()
		{
			_updateTimer += Time.deltaTime;
			if (_updateTimer < UPDATE_INTERVAL) return true;
			_updateTimer = 0f;
			return false;
		}

		protected override void UnsubscribeFromEvents()
		{
			_signalBus.Unsubscribe<AbilityRequestCreatedSignal>(OnAbilityRequestCreatedSignal);
		}
	}
}