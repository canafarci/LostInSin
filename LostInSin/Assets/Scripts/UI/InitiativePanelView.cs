using UnityEngine;
using UnityEngine.Serialization;

namespace LostInSin.UI
{
    public class InitiativePanelView : MonoBehaviour
    {
        [SerializeField] private Transform _initativePanelHolder;

        public Transform InitativePanelHolder => _initativePanelHolder;
    }
}