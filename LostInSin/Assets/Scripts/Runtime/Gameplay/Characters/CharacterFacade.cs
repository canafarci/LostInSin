using System.Collections.Generic;
using System.Runtime.InteropServices;
using LostInSin.Runtime.Gameplay.Abilities;
using LostInSin.Runtime.Gameplay.Signals;
using LostInSin.Runtime.Infrastructure.Signals;
using UnityEngine;
using VContainer;

namespace LostInSin.Runtime.Gameplay.Characters
{
	public class CharacterFacade : MonoBehaviour
	{
		[Inject] private Character _character;
		[Inject] private SignalBus _signalBus;

		public bool isPlayerCharacter => character.characterData.IsPlayerCharacter;
		public List<Ability> abilities => character.characterData.Abilities;
		public int actionPoints => character.currentActionPoints;
		public string characterName => character.characterName;
		public Character character => _character;

		public void SetAsActiveCharacter()
		{
			UnityEngine.Debug.Log($"Is Active {character.characterName}");
			_character.ResetActionPoints();
		}

		public void ReduceActionPoints(int abilityActionPointCost)
		{
			character.UseActionPoints(abilityActionPointCost);
			_signalBus.Fire(new CharacterAPChangedSignal());
		}
	}
}