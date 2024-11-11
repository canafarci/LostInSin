using LostInSin.Runtime.Gameplay.Characters;

namespace LostInSin.Runtime.Gameplay.Turns
{
	public interface ITurnModel
	{
		public CharacterFacade activeCharacter { get; set; }
	}
}