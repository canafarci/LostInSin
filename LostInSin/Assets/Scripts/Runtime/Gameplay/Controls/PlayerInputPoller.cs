using System;
using LostInSin.Runtime.Gameplay.Abilities;
using LostInSin.Runtime.Gameplay.Abilities.AbilityPlaying;
using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using LostInSin.Runtime.Gameplay.Signals;
using LostInSin.Runtime.Gameplay.Turns;
using LostInSin.Runtime.Gameplay.UI.AbilityPanel;
using LostInSin.Runtime.Infrastructure.Signals;
using VContainer.Unity;

namespace LostInSin.Runtime.Gameplay.Controls
{
	public class PlayerInputPoller : IInitializable, ITickable, IDisposable
	{
		private readonly SignalBus _signalBus;
		private readonly ITurnModel _turnModel;
		private readonly IAbilityPanelMediator _abilityPanelMediator;
		private readonly IAbilityPlayer _abilityPlayer;
		private Ability _ability;

		public PlayerInputPoller(SignalBus signalBus,
			ITurnModel turnModel,
			IAbilityPanelMediator abilityPanelMediator,
			IAbilityPlayer abilityPlayer)
		{
			_signalBus = signalBus;
			_turnModel = turnModel;
			_abilityPanelMediator = abilityPanelMediator;
			_abilityPlayer = abilityPlayer;
		}

		public void Initialize()
		{
			_abilityPanelMediator.OnAbilityClicked += AbilityClickedHandler;
			_signalBus.Subscribe<EndCharacterTurnSignal>(EndCharacterTurnSignalHandler);
		}

		private void EndCharacterTurnSignalHandler(EndCharacterTurnSignal signal)
		{
			_ability = null;
		}

		private void AbilityClickedHandler(Ability ability)
		{
			ability.AbilityRequest.Initialize(new AbilityRequestData());
			ability.AbilityRequest.StartRequest();

			if (ability.AbilityRequest.AbilityRequestType.HasFlag(AbilityRequestType.SelfTargeted))
			{
				ability.AbilityRequest.abilityRequestData.User = _turnModel.activeCharacter.character;
			}

			_ability = ability;
		}

		public void Tick()
		{
			if (_ability == null ||
			    _turnModel.activeCharacter == null ||
			    !_turnModel.activeCharacter.isPlayerCharacter) return;

			if (_ability.AbilityRequest.abilityRequestState == AbilityRequestState.Finished)
			{
				_ability.AbilityExecutionLogic.Initialize(_ability.AbilityRequest.abilityRequestData);
				_abilityPlayer.AddAbilityForPlaying(_ability.AbilityExecutionLogic);
				_ability = null;
				return;
			}

			if (_ability.AbilityRequest.abilityRequestState == AbilityRequestState.Continue)
			{
				_ability.AbilityRequest.UpdateRequest();
			}
		}

		public void Dispose()
		{
			_abilityPanelMediator.OnAbilityClicked -= AbilityClickedHandler;
		}
	}
}