using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Abilities.AbilityExecution;
using LostInSin.Runtime.Gameplay.Signals;
using LostInSin.Runtime.Infrastructure.Signals;
using VContainer;
using VContainer.Unity;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityPlaying
{
	public class AbilityPlayer : ITickable, IAbilityPlayer
	{
		[Inject] private SignalBus _signalBus;

		private Stack<AbilityExecutionLogic> _actionsToPlay = new();
		private AbilityExecutionLogic _currentPlayingAbility = null;

		public bool isPlaying => _currentPlayingAbility != null || _actionsToPlay.Count > 0;

		public void AddAbilityForPlaying(AbilityExecutionLogic abilityExecutionLogic) =>
			_actionsToPlay.Push(abilityExecutionLogic);

		public void Tick()
		{
			if (ShouldPlayNextAbility())
			{
				_currentPlayingAbility = _actionsToPlay.Pop();
				//start ability is called here
				_currentPlayingAbility.StartAbility();
			}

			if (_currentPlayingAbility != null) //ability could be null if there is no abilities to play
			{
				if (ShouldUpdateAbility())
				{
					_currentPlayingAbility.UpdateAbility();
				}

				if (ShouldFinishAbility())
				{
					TryPopNextAbility();
				}
			}
		}

		private bool ShouldPlayNextAbility() => _currentPlayingAbility == null && _actionsToPlay.Count > 0;

		private bool ShouldFinishAbility() => _currentPlayingAbility.executionStage == AbilityExecutionStage.Complete;

		private bool ShouldUpdateAbility() => _currentPlayingAbility.executionStage == AbilityExecutionStage.Updating;

		private void TryPopNextAbility()
		{
			if (_actionsToPlay.Count > 0)
				_currentPlayingAbility = _actionsToPlay.Pop();
			else
			{
				CheckUserAPAndAdvanceTurn(_currentPlayingAbility);
				_currentPlayingAbility = null;
			}
		}

		private void CheckUserAPAndAdvanceTurn(AbilityExecutionLogic currentPlayingAbility)
		{
			if (currentPlayingAbility.abilityRequestData.User.actionPoints == 0)
			{
				_signalBus.Fire(new EndCharacterTurnSignal());
			}
		}
	}
}