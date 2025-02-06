// Assets/Scripts/Characters/CharacterData.cs

using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Abilities;
using LostInSin.Runtime.Gameplay.Characters.Enums;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Characters
{
	[CreateAssetMenu(fileName = "CharacterData", menuName = "LostInSin/CharacterData")]
	public class CharacterData : ScriptableObject
	{
		public Sprite CharacterPortrait;
		public string CharacterName;
		public int MaxHealth;
		public int MaxActionPoints;
		public List<Ability> Abilities;
		public int Initiative;
		public TeamID TeamID;
		public GameObject Prefab;
	}
}