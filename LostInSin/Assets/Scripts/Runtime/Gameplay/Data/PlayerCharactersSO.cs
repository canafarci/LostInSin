using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Characters;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Data
{
	[CreateAssetMenu(fileName = "Player Characters", menuName = "LostInSin/Player Characters Holder", order = 0)]
	public class PlayerCharactersSO : ScriptableObject
	{
		public List<CharacterData> PlayerCharacters = new();
	}
}