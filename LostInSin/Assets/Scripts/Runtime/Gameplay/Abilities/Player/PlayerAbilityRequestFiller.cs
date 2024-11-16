using System;
using LostInSin.Runtime.Gameplay.Abilities.AbilityPlaying;
using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using LostInSin.Runtime.Gameplay.Signals;
using LostInSin.Runtime.Gameplay.Turns;
using LostInSin.Runtime.Gameplay.UI.AbilityPanel;
using LostInSin.Runtime.Infrastructure.Signals;
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
		private Ability _ability;
		private RaycastRequest _raycastRequest;

		public PlayerAbilityRequestFiller(SignalBus signalBus,
			ITurnModel turnModel,
			IAbilityPanelMediator abilityPanelMediator,
			IAbilityPlayer abilityPlayer,
			PlayerRaycaster playerRaycaster)
		{
			_signalBus = signalBus;
			_turnModel = turnModel;
			_abilityPanelMediator = abilityPanelMediator;
			_abilityPlayer = abilityPlayer;
			_playerRaycaster = playerRaycaster;
		}

		private bool CharacterHasEnoughAP(int abilityActionPointCost)
		{
			return _turnModel.activeCharacter.actionPoints >= abilityActionPointCost;
		}

		public void Tick()
		{
			if (_ability == null ||
			    _turnModel.activeCharacter == null ||
			    !_turnModel.activeCharacter.isPlayerCharacter) return;

			AbilityRequest abilityRequest = _ability.AbilityRequest;

			if (abilityRequest.abilityRequestState == AbilityRequestState.Complete)
			{
				SendAbilityForPlaying(abilityRequest);
				return;
			}

			if (abilityRequest.abilityRequestState == AbilityRequestState.Continue)
			{
				abilityRequest.UpdateRequest();

				if (AbilityIsRaycastLogic(abilityRequest))
				{
					CreateRaycastRequestOnMouseClick();
				}
			}
		}

		private static bool AbilityIsRaycastLogic(AbilityRequest abilityRequest)
		{
			return abilityRequest.AbilityRequestType.HasFlag(AbilityRequestType.PositionRaycasted) ||
			       abilityRequest.AbilityRequestType.HasFlag(AbilityRequestType.GridPositionRaycasted);
		}

		private void SendAbilityForPlaying(AbilityRequest abilityRequest)
		{
			_ability.AbilityExecutionLogic.Initialize(abilityRequest.abilityRequestData);
			_turnModel.activeCharacter.ReduceActionPoints(_ability.ActionPointCost);

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

		#region SUBSCIBTIONS_AND_UNSUBSCRIPTIONS

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
			if (!CharacterHasEnoughAP(ability.ActionPointCost)) return;
			if (_abilityPlayer.isPlaying) return;

			ability.AbilityRequest.Initialize(new AbilityRequestData());
			ability.AbilityRequest.StartRequest();

			ability.AbilityRequest.abilityRequestData.User = _turnModel.activeCharacter;

			_ability = ability;
		}

		public void Dispose()
		{
			_abilityPanelMediator.OnAbilityClicked -= AbilityClickedHandler;
		}

		#endregion
	}
}