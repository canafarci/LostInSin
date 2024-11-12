using LostInSin.Runtime.Gameplay.Abilities.AbilityExecution;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityPlaying
{
	public interface IAbilityPlayer
	{
		void AddAbilityForPlaying(AbilityExecutionLogic abilityExecutionLogic);
		bool isPlaying { get; }
	}
}