using Sirenix.OdinInspector;
using UnityEngine;
using LostInSin.Identifiers;

namespace LostInSin.Characters.PersistentData
{
    [CreateAssetMenu(fileName = "CharacterPersistentData", menuName = "Characters", order = 0)]
    public class CharacterPersistentData : SerializedScriptableObject
    {
        public CharacterClass CharacterClass;
        public AbilityIdentifiers[] CharacterAbilities;
    }
}