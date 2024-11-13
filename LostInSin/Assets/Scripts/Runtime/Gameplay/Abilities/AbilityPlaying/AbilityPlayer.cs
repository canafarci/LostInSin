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
			if (_currentPlayingAbility == null && _actionsToPlay.Count > 0)
			{
				_currentPlayingAbility = _actionsToPlay.Pop();
				_currentPlayingAbility.StartAction();
			}

			if (_currentPlayingAbility == null) return;

			if (_currentPlayingAbility.executionStage == AbilityExecutionStage.Updating)
			{
				_currentPlayingAbility.UpdateAction();
			}

			if (_currentPlayingAbility.executionStage == AbilityExecutionStage.Complete)
			{
				TryPopNextAbility();
			}
		}

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
			if (currentPlayingAbility.abilityRequestData.User.currentActionPoints == 0)
			{
				_signalBus.Fire(new EndCharacterTurnSignal());
			}
		}
	}
}