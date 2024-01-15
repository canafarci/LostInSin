using LostInSin.Abilities;
using Sirenix.OdinInspector;
using UnityEngine;
using LostInSin.Identifiers;

namespace LostInSin.Characters.PersistentData
{
    [CreateAssetMenu(fileName = "CharacterPersistentData", menuName = "Characters", order = 0)]
    public class CharacterPersistentData : SerializedScriptableObject
    {
        public bool DefaultSelectedCharacter = false;
        public CharacterClass CharacterClass;
        public AbilityInfo[] CharacterAbilities;
    }
}