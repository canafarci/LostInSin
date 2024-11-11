using UnityEngine;
using VContainer;

namespace LostInSin.Runtime.Gameplay.Characters
{
	public class CharacterFacade : MonoBehaviour
	{
		[Inject] private Character _character;

		public Character character => _character;

		public void SetAsActiveCharacter()
		{
			UnityEngine.Debug.Log($"Is Active {_character.characterName}");
		}
	}
}