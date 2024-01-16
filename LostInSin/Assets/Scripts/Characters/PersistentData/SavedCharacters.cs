using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace LostInSin.Characters.PersistentData
{
    [CreateAssetMenu(fileName = "SavedCharacters", menuName = "PersistentData/SavedCharacters", order = 0)]
    public class SavedCharacters : SerializedScriptableObject
    {
        [Inject]
        private void Init(DiContainer container)
        {
            PersistentCharacters.ForEach(x => { x.Inject(container); });
        }

        public List<CharacterPersistentData> PersistentCharacters;
    }
}