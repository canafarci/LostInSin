using LostInSin.Runtime.Gameplay.Abilities.AbilityPlaying;
using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using LostInSin.Runtime.Gameplay.Abilities.RequestFilling.RequestHandlers;
using LostInSin.Runtime.Gameplay.Pathfinding;
using LostInSin.Runtime.Gameplay.Signals;
using LostInSin.Runtime.Gameplay.TurnBasedCombat;
using LostInSin.Runtime.Gameplay.UI.AbilityPanel;
using LostInSin.Runtime.Infrastructure.Signals;
using System;
using LostInSin.Runtime.Gameplay.Characters.Enums;
using LostInSin.Runtime.Infrastructure.MemoryPool;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace LostInSin.Runtime.Gameplay.Abilities.RequestFilling
{
	public class PlayerAbilityRequestFiller : IInitializable, ITickable, IFixedTickable, IDisposable
	{
		private readonly SignalBus _signalBus;
		private readonly ITurnModel _turnModel;
		private readonly IAbilityPanelMediator _abilityPanelMediator;
		private readonly IAbilityPlayer _abilityPlayer;
		private readonly IPlayerAbilityRequestFillerModel _playerAbilityRequestFillerModel;

		// The currently selected Ability:
		private Ability _ability;
		private InputAction _cancelAction;
		public RaycastRequest RaycastRequest;

		public PlayerAbilityRequestFiller(
			SignalBus signalBus,
			ITurnModel turnModel,
			IAbilityPanelMediator abilityPanelMediator,
			IAbilityPlayer abilityPlayer,
			IPlayerAbilityRequestFillerModel playerAbilityRequestFillerModel)
		{
			_signalBus = signalBus;
			_turnModel = turnModel;
			_abilityPanelMediator = abilityPanelMediator;
			_abilityPlayer = abilityPlayer;
			_playerAbilityRequestFillerModel = playerAbilityRequestFillerModel;
		}

		#region IInitializable, ITickable, IFixedTickable

		public void Initialize()
		{
			_cancelAction = InputSystem.actions.FindAction("Cancel");
			// Subscribe to relevant signals / events
			_abilityPanelMediator.OnAbilityClicked += AbilityClickedHandler;
			_signalBus.Subscribe<ActiveTurnCharacterChangedSignal>(OnActiveTurnCharacterChangedSignal);
		}

		public void Tick()
		{
			// Early exit if there's no ability selected or if the conditions say "don't tick"
			if (_ability == null || ShouldNotTick()) return;

			//Cancel ability if the cancel input is registered
			if (_cancelAction.IsPressed())
			{
				OnAbilityCancelled();
				return;
			}

			AbilityRequest request = _ability.AbilityRequest;

			if (request.state == AbilityRequestState.Complete)
			{
				OnRequestComplete(request);
				return;
			}

			if (request.state == AbilityRequestState.Continue)
			{
				OnUpdateRequest(request);
				return;
			}

			if (request.state == AbilityRequestState.Cancelled)
			{
				OnAbilityCancelled();
				return;
			}
		}

		private void OnAbilityCancelled()
		{
			_signalBus.Fire(new AbilityRequestCancelledSignal());
			// Simply reset the selected ability
			_ability = null;
		}

		private void OnRequestComplete(AbilityRequest request)
		{
			// Check if we still have enough AP to perform the action
			if (CharacterHasEnoughAP(request.data.totalActionPointCost))
			{
				ExecuteAbility(request, _ability, request.data.totalActionPointCost);
			}

			// Whether or not we played it, we no longer need this ability reference
			_ability = null;
		}

		private void OnUpdateRequest(AbilityRequest request)
		{
			// Update the request (refresh internal data)
			request.UpdateRequest();

			// Create a raycast request when left-click is detected
			if (Input.GetMouseButtonDown(0))
			{
				RaycastRequest = new(Input.mousePosition);
			}

			// Run the chain of responsibility to handle all relevant AbilityRequestType flags
			_playerAbilityRequestFillerModel.updateAbilityRequestTypeChain.Handle(request);
		}

		public void FixedTick()
		{
			// Run the chain of responsibility to handle all relevant AbilityRequestType flags
			if (_ability?.AbilityRequest != null)
			{
				_playerAbilityRequestFillerModel.fixedUpdateAbilityRequestTypeChain.Handle(_ability.AbilityRequest);
			}

			// At the end of chain, mark the raycast as processed to disable it for processing again
			if (RaycastRequest != null)
			{
				RaycastRequest.isProcessed = true;
			}
		}

		public void Dispose()
		{
			_abilityPanelMediator.OnAbilityClicked -= AbilityClickedHandler;
			_signalBus.Unsubscribe<ActiveTurnCharacterChangedSignal>(OnActiveTurnCharacterChangedSignal);
		}

		#endregion

		#region Chain / Request Logic Helpers

		/// <summary>
		/// Checks if we should skip the 'Tick' process entirely.
		/// </summary>
		private bool ShouldNotTick()
		{
			return _ability == null
			       || _turnModel.activeCharacter == null
			       || _turnModel.activeCharacter.teamID != TeamID.Player;
		}

		/// <summary>
		/// Checks if character has enough action points for a cost.
		/// </summary>
		private bool CharacterHasEnoughAP(int cost)
		{
			return _turnModel.activeCharacter.actionPoints >= cost;
		}

		/// <summary>
		/// Then schedules the ability for execution/animation.
		/// </summary>
		private void ExecuteAbility(AbilityRequest request, Ability ability, int cost)
		{
			_ability.AbilityExecution.Initialize(request.data);
			_turnModel.activeCharacter.ReduceActionPoints(cost);
			_abilityPlayer.AddAbilityForPlaying(ability.AbilityExecution);

			PoolManager.ReleasePure(request.data);
		}

		#endregion

		#region Event Handlers

		/// <summary>
		/// Called whenever the active turn character changes.
		/// We might want to reset our _ability selection if it was for a different character.
		/// </summary>
		private void OnActiveTurnCharacterChangedSignal(ActiveTurnCharacterChangedSignal signal)
		{
			OnAbilityCancelled();
		}

		/// <summary>
		/// Called whenever an ability is clicked on the UI panel.
		/// </summary>
		private void AbilityClickedHandler(Ability ability)
		{
			// Cancel previous ability, if any
			OnAbilityCancelled();

			// If we don't have enough AP to use this ability, do nothing:
			if (!CharacterHasEnoughAP(ability.AbilityRequest.DefaultActionPointCost)) return;

			// If the ability player is currently executing an ability, do nothing:
			if (_abilityPlayer.isPlaying) return;

			// Initialize the request
			ability.AbilityRequest.Initialize();
			ability.AbilityRequest.data.User = _turnModel.activeCharacter;

			// Now "select" this ability
			_ability = ability;

			// Fire a signal to show any request visuals:
			_signalBus.Fire(new AbilityRequestCreatedSignal(_ability.AbilityRequest));
		}

		#endregion
	}
}