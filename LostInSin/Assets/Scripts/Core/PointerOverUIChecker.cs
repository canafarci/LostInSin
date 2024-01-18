using UnityEngine.EventSystems;
using Zenject;

namespace LostInSin.Core
{
    public class PointerOverUIChecker : ITickable
    {
        private bool _pointerIsOverUI = false;

        public bool PointerIsOverUI => _pointerIsOverUI;

        public void Tick()
        {
            _pointerIsOverUI = EventSystem.current.IsPointerOverGameObject();
        }
    }
}