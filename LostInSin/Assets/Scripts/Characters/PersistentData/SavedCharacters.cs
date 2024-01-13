using System.Collections.Generic;
using UnityEngine;

namespace LostInSin.Characters.PersistentData
{
    [CreateAssetMenu(fileName = "SavedCharacters", menuName = "PersistentData/SavedCharacters", order = 0)]
    public class SavedCharacters : ScriptableObject
    {
        public List<CharacterPersistentData> PersistentCharacters;
    }
}