using UnityEngine;

namespace LostInSin.UI.CharacterSelectPanel
{
    public class CharacterSelectPanelView : MonoBehaviour
    {
        [SerializeField] private RectTransform _characterSelectIconHolder;

        public RectTransform CharacterSelectIconHolder => _characterSelectIconHolder;
    }
}