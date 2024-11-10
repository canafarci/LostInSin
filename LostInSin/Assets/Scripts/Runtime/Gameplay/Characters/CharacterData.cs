// Assets/Scripts/Characters/CharacterData.cs

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
		public Ability[] Abilities;
		public int Initiative;
		public GameObject Prefab;
	}
}