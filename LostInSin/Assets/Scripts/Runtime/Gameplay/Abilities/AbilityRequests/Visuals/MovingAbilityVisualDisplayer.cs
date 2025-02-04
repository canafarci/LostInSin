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
	public abstract class MovingAbilityVisualDisplayer : SignalListener, IFixedTickable
	{
		protected AbilityRequest _abilityRequest;
		protected readonly PlayerRaycaster _playerRaycaster;

		private readonly IGridPathfinder _gridPathfinder;
		private readonly LineRenderer _lineRenderer;

		private const float UPDATE_INTERVAL = 0.05f;
		private float _updateTimer;

		private readonly Color _validColor = new(0f, 0.7f, 0f, 0.8f);
		private readonly Color _invalidColor = new(0.7f, 0f, 0f, 0.8f);
		private Color _currentColor;

		protected MovingAbilityVisualDisplayer(IGridPathfinder gridPathfinder, PlayerRaycaster playerRaycaster)
		{
			_gridPathfinder = gridPathfinder;
			_playerRaycaster = playerRaycaster;

			// Create a new GameObject for the LineRenderer
			_lineRenderer = new GameObject($"{GetType().Name}_LineRenderer").AddComponent<LineRenderer>();
			_lineRenderer.startWidth = 0.1f;
			_lineRenderer.endWidth = 0.1f;
			_lineRenderer.material = new(Shader.Find("Sprites/Default"));
		}

		protected override void SubscribeToEvents()
		{
			// Subscribe to relevant signals
			_signalBus.Subscribe<AbilityRequestCreatedSignal>(OnAbilityRequestCreatedSignal);
			_signalBus.Subscribe<AbilityRequestCancelledSignal>(OnAbilityRequestCancelledSignalHandler);
		}

		protected override void UnsubscribeFromEvents()
		{
			// Unsubscribe from signals
			_signalBus.Unsubscribe<AbilityRequestCreatedSignal>(OnAbilityRequestCreatedSignal);
			_signalBus.Unsubscribe<AbilityRequestCancelledSignal>(OnAbilityRequestCancelledSignalHandler);
		}

		public void FixedTick()
		{
			if (ShouldNotDraw()) return;
			OnFixedTickDisplay();
		}

		/// <summary>
		/// Child classes can override this to define what happens during FixedTick
		/// when drawing is allowed.
		/// </summary>
		protected virtual void OnFixedTickDisplay()
		{
			// By default, try to raycast for a grid cell
			if (_playerRaycaster.TryRaycastForGridCell(out GridCell targetCell))
			{
				DisplayPathAsLine(targetCell);
			}
		}

		/// <summary>
		///  Handler for AbilityRequestCreatedSignal. It checks if the request
		///  is relevant for the subclass and sets _abilityRequest if so.
		/// </summary>
		private void OnAbilityRequestCreatedSignal(AbilityRequestCreatedSignal signal)
		{
			AbilityRequest request = signal.request;
			if (!IsRequestRelevant(request)) return;

			_abilityRequest = request;
		}

		private void OnAbilityRequestCancelledSignalHandler(AbilityRequestCancelledSignal signal)
		{
			_abilityRequest = null;
			ClearLineRenderer();
		}

		/// <summary>
		/// Each subclass decides if the given AbilityRequest is relevant (e.g., 
		/// whether it's a melee request, a move request, etc.)
		/// </summary>
		protected abstract bool IsRequestRelevant(AbilityRequest request);

		/// <summary>
		/// Calculates the action point cost based on the path and the request. 
		/// Subclasses can provide different logic.
		/// </summary>
		protected abstract int CalculateActionPointCost(List<GridCell> path, CharacterFacade user);

		/// <summary>
		/// Displays the path as a line from the starting cell to the target cell.
		/// </summary>
		protected void DisplayPathAsLine(GridCell targetCell)
		{
			if (_abilityRequest == null) return;

			CharacterFacade character = _abilityRequest.data.User;
			if (_gridPathfinder.FindPath(character.currentCell, targetCell, out List<GridCell> path))
			{
				Vector3[] positions = new Vector3[path.Count];
				for (int i = 0; i < path.Count; i++)
				{
					// Small offset to ensure the line is above ground
					positions[i] = path[i].centerPosition + Vector3.up * 0.15f;
				}

				_lineRenderer.positionCount = positions.Length;
				_lineRenderer.SetPositions(positions);

				ChangeColorBasedOnPlayerAP(path, character);
			}
			else
			{
				ClearLineRenderer();
			}
		}

		/// <summary>
		/// Changes the line color based on whether the user has enough AP 
		/// for the path cost.
		/// </summary>
		protected void ChangeColorBasedOnPlayerAP(List<GridCell> path, CharacterFacade characterFacade)
		{
			int apCost = CalculateActionPointCost(path, characterFacade);

			// If cost is higher than player's AP, mark in red, otherwise in green
			bool isInvalid = apCost > characterFacade.actionPoints;
			Color desiredColor = isInvalid ? _invalidColor : _validColor;

			if (_currentColor != desiredColor)
			{
				_lineRenderer.startColor = desiredColor;
				_lineRenderer.endColor = desiredColor;
				_currentColor = desiredColor;
			}
		}

		protected void ClearLineRenderer()
		{
			_lineRenderer.positionCount = 0;
		}

		/// <summary>
		/// Returns true if we should not display the line. 
		/// Example reasons: no request, request is complete, 
		/// pointer over UI, or not yet time to update.
		/// </summary>
		private bool ShouldNotDraw()
		{
			if (_abilityRequest == null) return true;

			if (_abilityRequest.state == AbilityRequestState.Complete)
			{
				ClearLineRenderer();
				return true;
			}

			if (EventSystem.current.IsPointerOverGameObject()) return true;

			_updateTimer += Time.deltaTime;
			if (_updateTimer < UPDATE_INTERVAL) return true;
			_updateTimer = 0f;

			return false;
		}
	}
}