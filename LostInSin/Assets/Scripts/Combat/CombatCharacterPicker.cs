using System.Collections.Generic;
using LostInSin.Characters;
using Sirenix.Utilities;
using UnityEngine;

namespace LostInSin.Combat
{
    /// <summary>
    /// This is a test version of this class
    /// real implementation would check whether enemy characters in the area
    /// which know the position of the player team characters
    /// </summary>
    public class CombatCharacterPicker
    {
        private const int CHARACTER_LAYER_MASK = 1 << 6;

        public List<Character> GetCombatCharacters()
        {
            RaycastHit[] results = new RaycastHit[25];

            int size = Physics.BoxCastNonAlloc(Vector3.up * 1000f,
                                               Vector3.one * 100f,
                                               Vector3.down,
                                               results,
                                               Quaternion.identity,
                                               Mathf.Infinity,
                                               CHARACTER_LAYER_MASK);

            List<Character> characters = new();

            for (int i = 0; i < size; i++) characters.Add(results[i].transform.GetComponent<Character>());

            Debug.Log(characters.Count);
            return characters;
        }
    }
}