using LostInSin.Runtime.Gameplay.Abilities;
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
		public AbilityExecution playingAbility => _abilityPlayer.playingAbility;
		public CharacterFacade activeCharacter => _turnModel.activeCharacter;

		public void PlayAbility(Ability ability)
		{
			ability.AbilityExecution.Initialize(ability.AbilityRequest.data);

			int abilityActionPointCost = ability.AbilityRequest.data.totalActionPointCost;
			ability.AbilityExecution.executionData.User.ReduceActionPoints(abilityActionPointCost);

			_abilityPlayer.AddAbilityForPlaying(ability.AbilityExecution);
		}
	}
}