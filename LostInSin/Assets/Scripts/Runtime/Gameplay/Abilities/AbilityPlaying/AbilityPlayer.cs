using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Abilities.AbilityExecution;
using VContainer.Unity;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityPlaying
{
	public class AbilityPlayer : ITickable, IAbilityPlayer
	{
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
				if (_actionsToPlay.Count > 0)
					_currentPlayingAbility = _actionsToPlay.Pop();
				else
					_currentPlayingAbility = null;
			}
		}
	}
}