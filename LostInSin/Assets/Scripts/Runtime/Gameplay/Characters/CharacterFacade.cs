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

		public bool isPlayerCharacter => _character.characterData.IsPlayerCharacter;
		public List<Ability> abilities => _character.characterData.Abilities;
		public int actionPoints => _character.currentActionPoints;
		public int initiative => _character.initiative;
		public string characterName => _character.characterName;

		public void SetAsActiveCharacter()
		{
			UnityEngine.Debug.Log($"Is Active {_character.characterName}");
			_character.ResetActionPoints();
		}

		public void ReduceActionPoints(int abilityActionPointCost)
		{
			_character.UseActionPoints(abilityActionPointCost);
			_signalBus.Fire(new CharacterAPChangedSignal());
		}
	}
}