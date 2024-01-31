using UnityEngine;

namespace LostInSin.UI
{
    public class CharacterSelectPanelView : MonoBehaviour
    {
        [SerializeField] private RectTransform _characterSelectIconHolder;

        public RectTransform CharacterSelectIconHolder => _characterSelectIconHolder;
    }
}