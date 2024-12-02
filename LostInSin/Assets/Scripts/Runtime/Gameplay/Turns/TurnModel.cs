using System;
using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Characters;
using R3;

namespace LostInSin.Runtime.Gameplay.Turns
{
	public class TurnModel : ITurnModel
	{
		public CharacterFacade activeCharacter { get; set; }
		public LinkedList<CharacterFacade> characterTurnQueue { get; } = new();
	}
}