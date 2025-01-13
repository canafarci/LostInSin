using LostInSin.Runtime.Gameplay.Abilities.AbilityExecutions;
using LostInSin.Runtime.Gameplay.Abilities.AbilityPlaying;
using LostInSin.Runtime.Gameplay.Characters;
using VContainer;

namespace LostInSin.Runtime.Gameplay.TurnBasedCombat
{
	public class TurnSystemFacade
	{
		[Inject] private IAbilityPlayer _abilityPlayer;
		[Inject] private ITurnModel _turnModel;

		public bool isPlayingAbility => _abilityPlayer.isPlaying;
		public CharacterFacade activeCharacter => _turnModel.activeCharacter;

		public void AddAbilityForPlaying(AbilityExecution abilityExecution) =>
			_abilityPlayer.AddAbilityForPlaying(abilityExecution);
	}
}