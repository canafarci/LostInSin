using LostInSin.Runtime.Gameplay.Abilities.RequestFilling;
using LostInSin.Runtime.Gameplay.Characters;
using LostInSin.Runtime.Gameplay.Grid.Data;
using LostInSin.Runtime.Gameplay.Pathfinding;
using LostInSin.Runtime.Gameplay.Signals;
using LostInSin.Runtime.Infrastructure.Templates;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer.Unity;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityRequests.Visuals
{
	public class MoveAbilityVisualDisplayer : SignalListener, IFixedTickable
	{
		private AbilityRequest _abilityRequest;
		private readonly IGridPathfinder _gridPathfinder;
		private readonly PlayerRaycaster _playerRaycaster;
		private readonly LineRenderer _lineRenderer;
		private const float UPDATE_INTERVAL = 0.05f;
		private float _updateTimer = 0f;

		private readonly Color _validColor = new Color(0f, .7f, 0f, .8f);
		private readonly Color _invalidColor = new Color(.7f, 0f, 0f, .8f);
		private Color _currentColor;

		public MoveAbilityVisualDisplayer(IGridPathfinder gridPathfinder,
			PlayerRaycaster playerRaycaster)
		{
			_gridPathfinder = gridPathfinder;
			_playerRaycaster = playerRaycaster;
			_lineRenderer = new GameObject("LineRenderer").AddComponent<LineRenderer>();

			_lineRenderer.startWidth = 0.1f;
			_lineRenderer.endWidth = 0.1f;
			_lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
		}

		protected override void SubscribeToEvents()
		{
			_signalBus.Subscribe<AbilityRequestCreatedSignal>(OnAbilityRequestCreatedSignal);
			_signalBus.Subscribe<ActiveTurnCharacterChangedSignal>(OnActiveTurnCharacterChangedSignal);
		}

		private void OnActiveTurnCharacterChangedSignal(ActiveTurnCharacterChangedSignal signal)
		{
			ClearLineRenderer();
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

			if (_playerRaycaster.TryRaycastForGridCell(out GridCell targetCell))
			{
				DisplayPathAsLine(targetCell);
			}
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
			CharacterFacade characterFacade = _abilityRequest.data.User;
			if (_gridPathfinder.FindPath(characterFacade.currentCell, targetCell, out List<GridCell> path))
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

				ChangeColorBasedOnPlayerAP(path, characterFacade);
			}
			else
			{
				ClearLineRenderer();
			}
		}

		private void ChangeColorBasedOnPlayerAP(List<GridCell> path, CharacterFacade characterFacade)
		{
			int apCost = _abilityRequest.data.DefaultActionPointCost + path.Count - 1; //-1 because cell count is one less than point count

			if (apCost > characterFacade.actionPoints && _currentColor != _invalidColor)
			{
				_lineRenderer.startColor = _invalidColor;
				_lineRenderer.endColor = _invalidColor;
				_currentColor = _invalidColor;
			}
			else if (apCost <= characterFacade.actionPoints && _currentColor != _validColor)
			{
				_lineRenderer.startColor = _validColor;
				_lineRenderer.endColor = _validColor;
				_currentColor = _validColor;
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
			_signalBus.Unsubscribe<ActiveTurnCharacterChangedSignal>(OnActiveTurnCharacterChangedSignal);
		}
	}
}