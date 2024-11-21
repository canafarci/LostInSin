using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Abilities.AbilityExecution;
using LostInSin.Runtime.Gameplay.Signals;
using LostInSin.Runtime.Infrastructure.Templates;
using VContainer.Unity;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityPlaying
{
	public class AbilityPlayer : SignalListener, ITickable, IAbilityPlayer
	{
		private Stack<AbilityExecution.AbilityExecution> _actionsToPlay = new();
		private AbilityExecution.AbilityExecution _currentPlayingAbility = null;

		public bool isPlaying => _currentPlayingAbility != null || _actionsToPlay.Count > 0;

		public void AddAbilityForPlaying(AbilityExecution.AbilityExecution abilityExecution) =>
			_actionsToPlay.Push(abilityExecution);

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
					_currentPlayingAbility.EndAbility();
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

		private void CheckUserAPAndAdvanceTurn(AbilityExecution.AbilityExecution currentPlayingAbility)
		{
			if (currentPlayingAbility.requestData.User.actionPoints == 0)
			{
				_signalBus.Fire(new EndCharacterTurnSignal());
			}
		}

		protected override void SubscribeToEvents()
		{
			_signalBus.Subscribe<AnimationEventSignal>(OnAnimationEventSignalHandler);
		}

		private void OnAnimationEventSignalHandler(AnimationEventSignal signal)
		{
			_currentPlayingAbility.executionData.AbilityTriggers.Add(signal.stringAsset);
		}

		protected override void UnsubscribeFromEvents()
		{
			_signalBus.Unsubscribe<AnimationEventSignal>(OnAnimationEventSignalHandler);
		}
	}
}