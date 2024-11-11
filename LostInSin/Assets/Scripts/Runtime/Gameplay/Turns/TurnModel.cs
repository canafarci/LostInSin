using LostInSin.Runtime.Gameplay.Characters;

namespace LostInSin.Runtime.Gameplay.Turns
{
	public class TurnModel : ITurnModel
	{
		public CharacterFacade activeCharacter { get; set; }
	}
}