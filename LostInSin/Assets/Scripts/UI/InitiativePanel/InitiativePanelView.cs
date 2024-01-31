using UnityEngine;

namespace LostInSin.UI.InitiativePanel
{
    public class InitiativePanelView : MonoBehaviour
    {
        [SerializeField] private RectTransform _initativePanelHolder;

        public RectTransform InitativePanelHolder => _initativePanelHolder;
    }
}