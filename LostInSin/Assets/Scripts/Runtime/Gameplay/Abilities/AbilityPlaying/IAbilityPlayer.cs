using LostInSin.Runtime.Gameplay.Abilities.AbilityExecution;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityPlaying
{
	public interface IAbilityPlayer
	{
		void AddAbilityForPlaying(AbilityExecution.AbilityExecution abilityExecution);
		bool isPlaying { get; }
	}
}