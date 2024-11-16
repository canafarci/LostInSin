using System;
using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Abilities.AbilityPlaying;
using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using LostInSin.Runtime.Gameplay.Signals;
using LostInSin.Runtime.Gameplay.Turns;
using LostInSin.Runtime.Gameplay.UI.AbilityPanel;
using LostInSin.Runtime.Grid.Data;
using LostInSin.Runtime.Infrastructure.Signals;
using LostInSin.Runtime.Pathfinding;
using UnityEngine;
using VContainer.Unity;

namespace LostInSin.Runtime.Gameplay.Abilities.Player
{
	public class PlayerAbilityRequestFiller : IInitializable, ITickable, IFixedTickable, IDisposable
	{
		private readonly SignalBus _signalBus;
		private readonly ITurnModel _turnModel;
		private readonly IAbilityPanelMediator _abilityPanelMediator;
		private readonly IAbilityPlayer _abilityPlayer;
		private readonly PlayerRaycaster _playerRaycaster;
		private readonly IGridPathfinder _gridPathfinder;
		private Ability _ability;
		private RaycastRequest _raycastRequest;

		public PlayerAbilityRequestFiller(SignalBus signalBus,
			ITurnModel turnModel,
			IAbilityPanelMediator abilityPanelMediator,
			IAbilityPlayer abilityPlayer,
			PlayerRaycaster playerRaycaster,
			IGridPathfinder gridPathfinder)
		{
			_signalBus = signalBus;
			_turnModel = turnModel;
			_abilityPanelMediator = abilityPanelMediator;
			_abilityPlayer = abilityPlayer;
			_playerRaycaster = playerRaycaster;
			_gridPathfinder = gridPathfinder;
		}

		private bool CharacterHasEnoughAP(int abilityActionPointCost)
		{
			return _turnModel.activeCharacter.actionPoints >= abilityActionPointCost;
		}

		public void Tick()
		{
			if (ShouldNotTick()) return;

			AbilityRequest abilityRequest = _ability.AbilityRequest;

			if (abilityRequest.state == AbilityRequestState.Complete)
			{
				SendAbilityForPlaying(abilityRequest);
				return;
			}

			if (abilityRequest.state == AbilityRequestState.Continue)
			{
				abilityRequest.UpdateRequest();

				if (AbilityIsRaycastLogic(abilityRequest))
				{
					CreateRaycastRequestOnMouseClick();
				}

				if (AbilityIsGridPathfindingLogic(abilityRequest))
				{
					TryFindPath(abilityRequest);
				}
			}
		}

		private void TryFindPath(AbilityRequest abilityRequest)
		{
			if (_gridPathfinder.FindPath(abilityRequest, out List<GridCell> pathCells))
			{
				abilityRequest.data.PathCells = pathCells;
			}
		}

		private bool AbilityIsGridPathfindingLogic(AbilityRequest abilityRequest)
		{
			return abilityRequest.RequestType.HasFlag(AbilityRequestType.GridPathFinding);
		}

		private bool ShouldNotTick()
		{
			return _ability == null ||
			       _turnModel.activeCharacter == null ||
			       !_turnModel.activeCharacter.isPlayerCharacter;
		}

		private static bool AbilityIsRaycastLogic(AbilityRequest abilityRequest)
		{
			return abilityRequest.RequestType.HasFlag(AbilityRequestType.PositionRaycasted) ||
			       abilityRequest.RequestType.HasFlag(AbilityRequestType.GridPositionRaycasted);
		}

		private void SendAbilityForPlaying(AbilityRequest abilityRequest)
		{
			_ability.AbilityExecutionLogic.Initialize(abilityRequest.data);
			_turnModel.activeCharacter.ReduceActionPoints(_ability.DefaultActionPointCost);

			_abilityPlayer.AddAbilityForPlaying(_ability.AbilityExecutionLogic);

			_ability = null;
		}

		private void CreateRaycastRequestOnMouseClick()
		{
			if (Input.GetMouseButtonDown(0))
			{
				_raycastRequest = new RaycastRequest(Input.mousePosition);
			}
		}

		public void FixedTick()
		{
			if (_raycastRequest == null || _raycastRequest.isProcessed) return;

			AbilityRequest abilityRequest = _ability.AbilityRequest;
			_playerRaycaster.TryRaycast(abilityRequest, ref _raycastRequest);
		}

		#region SUBSCRIPTIONS_AND_UNSUBSCRIPTIONS

		public void Initialize()
		{
			_abilityPanelMediator.OnAbilityClicked += AbilityClickedHandler;
			_signalBus.Subscribe<ActiveTurnCharacterChangedSignal>(OnActiveTurnCharacterChangedSignal);
		}

		private void OnActiveTurnCharacterChangedSignal(ActiveTurnCharacterChangedSignal signal)
		{
			_ability = null;
		}

		private void AbilityClickedHandler(Ability ability)
		{
			if (!CharacterHasEnoughAP(ability.DefaultActionPointCost)) return;
			if (_abilityPlayer.isPlaying) return;

			ability.AbilityRequest.Initialize(new AbilityRequestData());
			ability.AbilityRequest.StartRequest();

			ability.AbilityRequest.data.User = _turnModel.activeCharacter;

			_ability = ability;
			//used to create visuals for the request, e.g. drawing a path to display the movement
			_signalBus.Fire(new AbilityRequestCreatedSignal(_ability.AbilityRequest));
		}

		public void Dispose()
		{
			_abilityPanelMediator.OnAbilityClicked -= AbilityClickedHandler;
		}

		#endregion
	}
}