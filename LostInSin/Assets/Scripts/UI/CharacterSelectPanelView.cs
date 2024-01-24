using UnityEngine;

namespace LostInSin.UI
{
    public class CharacterSelectPanelView : MonoBehaviour
    {
        [SerializeField] private Transform _characterSelectIconHolder;

        public Transform CharacterSelectIconHolder => _characterSelectIconHolder;
    }
}