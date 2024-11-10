using LostInSin.Runtime.Gameplay.Characters;

namespace LostInSin.Runtime.Gameplay.Abilities
{
	public interface IAbilityCommand
	{
		public void Execute(Character user, Character target);
	}
}