using UnityEngine;

namespace LostInSin.UI.Data
{
    [CreateAssetMenu(fileName = "Character Select Panel Data", menuName = "LostInSin/UI/Character Select", order = 0)]
    public class CharacterSelectPanelData : ScriptableObject
    {
        public CharacterSelectPanelIconView CharacterSelectIconPrefab;
    }
}