using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Abilities;
using UnityEngine;
using VContainer;

namespace LostInSin.Runtime.Gameplay.Characters
{
	public class CharacterFacade : MonoBehaviour
	{
		[Inject] private Character _character;

		public Character character => _character;
		public bool isPlayerCharacter => _character.characterData.IsPlayerCharacter;
		public List<Ability> abilities => _character.characterData.Abilities;

		public void SetAsActiveCharacter()
		{
			UnityEngine.Debug.Log($"Is Active {_character.characterName}");
		}
	}
}