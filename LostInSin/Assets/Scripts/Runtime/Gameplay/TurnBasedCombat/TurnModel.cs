using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Characters;

namespace LostInSin.Runtime.Gameplay.TurnBasedCombat
{
	public class TurnModel : ITurnModel
	{
		public CharacterFacade activeCharacter { get; set; }
		public LinkedList<CharacterFacade> characterTurnQueue { get; } = new();
	}
}