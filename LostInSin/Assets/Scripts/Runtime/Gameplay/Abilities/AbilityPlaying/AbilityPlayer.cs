using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Abilities.AbilityExecutions;
using LostInSin.Runtime.Gameplay.Signals;
using LostInSin.Runtime.Infrastructure.Templates;
using VContainer.Unity;
using UnityEngine; // for Time.deltaTime (if not already included)

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityPlaying
{
	public interface IAbilityPlayer
	{
		void AddAbilityForPlaying(AbilityExecution abilityExecution);
		bool isPlaying { get; }
		AbilityExecution playingAbility { get; }
	}

	public class AbilityPlayer : SignalListener, ITickable, IAbilityPlayer
	{
		private AbilityExecution _currentPlayingAbility = null;

		// --- NEW FIELDS ---
		private float _cooldownTimeRemaining = 0f; // How long until isPlaying becomes false after ability ends
		private const float COOLDOWN_DURATION = 0.5f; // 0.5 second cooldown

		// Instead of checking _currentPlayingAbility != null, we also factor in the cooldown time
		public bool isPlaying => _currentPlayingAbility != null || _cooldownTimeRemaining > 0f;

		public AbilityExecution playingAbility => _currentPlayingAbility;

		public void AddAbilityForPlaying(AbilityExecution abilityExecution)
		{
			_currentPlayingAbility = abilityExecution;
			// Reset any cooldown if a new ability starts
			_cooldownTimeRemaining = 0f;
			_currentPlayingAbility.StartAbility();
		}

		public void Tick()
		{
			// Decrement cooldown timer if active
			if (_cooldownTimeRemaining > 0f)
			{
				_cooldownTimeRemaining -= Time.deltaTime;
				if (_cooldownTimeRemaining < 0f)
				{
					_cooldownTimeRemaining = 0f;
				}
			}

			// If no ability is running, no need to proceed
			if (_currentPlayingAbility == null)
			{
				return;
			}

			// Normal ability life-cycle checks
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

				// Start the cooldown period now that the ability has ended
				_cooldownTimeRemaining = COOLDOWN_DURATION;
			}
		}

		private bool ShouldUpdateAbility() => _currentPlayingAbility.executionStage == AbilityExecutionStage.Updating;
		private bool ShouldFinishAbility() => _currentPlayingAbility.executionStage == AbilityExecutionStage.Finishing;
		private bool ShouldEndAbility() => _currentPlayingAbility.executionStage == AbilityExecutionStage.Complete;

		private void CheckEndTurn()
		{
			CheckUserAPAndAdvanceTurn(_currentPlayingAbility);
			// Clear the current ability now that it has ended
			_currentPlayingAbility = null;
		}

		private void CheckUserAPAndAdvanceTurn(AbilityExecution currentPlayingAbility)
		{
			if (currentPlayingAbility.executionData.User.actionPoints == 0)
			{
				_signalBus.Fire(new EndCharacterTurnSignal());
			}
		}

		private void FireAbilityExecutionEndedSignal()
			=> _signalBus.Fire(new AbilityExecutionCompletedSignal());

		protected override void SubscribeToEvents()
		{
			_signalBus.Subscribe<AnimationEventSignal>(OnAnimationEventSignalHandler);
		}

		private void OnAnimationEventSignalHandler(AnimationEventSignal signal)
		{
			if (_currentPlayingAbility != null)
			{
				_currentPlayingAbility.executionData.AbilityTriggers.Add(signal.stringAsset);
			}
		}

		protected override void UnsubscribeFromEvents()
		{
			_signalBus.Unsubscribe<AnimationEventSignal>(OnAnimationEventSignalHandler);
		}
	}
}