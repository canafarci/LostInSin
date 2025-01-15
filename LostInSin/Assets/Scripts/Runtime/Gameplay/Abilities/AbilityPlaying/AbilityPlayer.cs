using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Abilities.AbilityExecutions;
using LostInSin.Runtime.Gameplay.Signals;
using LostInSin.Runtime.Infrastructure.Templates;
using VContainer.Unity;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityPlaying
{
	public interface IAbilityPlayer
	{
		void AddAbilityForPlaying(AbilityExecution abilityExecution);
		bool isPlaying { get; }
	}

	public class AbilityPlayer : SignalListener, ITickable, IAbilityPlayer
	{
		private AbilityExecution _currentPlayingAbility = null;

		public bool isPlaying => _currentPlayingAbility != null;

		public void AddAbilityForPlaying(AbilityExecution abilityExecution)
		{
			_currentPlayingAbility = abilityExecution;
			_currentPlayingAbility.StartAbility();
		}

		public void Tick()
		{
			if (_currentPlayingAbility != null) //ability could be null if there is no abilities to play
			{
				if (ShouldUpdateAbility())
				{
					_currentPlayingAbility.UpdateAbility();
				}

				if (ShouldFinishAbility())
				{
					_currentPlayingAbility.FinishAbility();
				}

				if (ShouldEndAbility())
				{
					_currentPlayingAbility.EndAbility();
					CheckEndTurn();
					FireAbilityExecutionEndedSignal();
				}
			}
		}

		private bool ShouldUpdateAbility() => _currentPlayingAbility.executionStage == AbilityExecutionStage.Updating;
		private bool ShouldFinishAbility() => _currentPlayingAbility.executionStage == AbilityExecutionStage.Finishing;
		private bool ShouldEndAbility() => _currentPlayingAbility.executionStage == AbilityExecutionStage.Complete;

		private void CheckEndTurn()
		{
			CheckUserAPAndAdvanceTurn(_currentPlayingAbility);
			_currentPlayingAbility = null;
		}

		private void CheckUserAPAndAdvanceTurn(AbilityExecution currentPlayingAbility)
		{
			if (currentPlayingAbility.executionData.User.actionPoints == 0)
			{
				_signalBus.Fire(new EndCharacterTurnSignal());
			}
		}

		private void FireAbilityExecutionEndedSignal() => _signalBus.Fire(new AbilityExecutionCompletedSignal());

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