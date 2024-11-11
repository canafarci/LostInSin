// Assets/Scripts/Characters/CharacterData.cs

using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Abilities;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Characters
{
	[CreateAssetMenu(fileName = "CharacterData", menuName = "LostInSin/CharacterData")]
	public class CharacterData : ScriptableObject
	{
		public string CharacterName;
		public int MaxHealth;
		public int MaxActionPoints;
		public List<Ability> Abilities;
		public int Initiative;
		public bool IsPlayerCharacter;
		public GameObject Prefab;
	}
}