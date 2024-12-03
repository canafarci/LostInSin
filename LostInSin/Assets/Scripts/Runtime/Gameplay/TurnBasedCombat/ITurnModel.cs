using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Characters;

namespace LostInSin.Runtime.Gameplay.TurnBasedCombat
{
	public interface ITurnModel
	{
		public CharacterFacade activeCharacter { get; set; }
		public LinkedList<CharacterFacade> characterTurnQueue { get; }
	}
}