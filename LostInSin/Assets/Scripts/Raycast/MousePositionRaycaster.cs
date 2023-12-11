using UnityEngine;
using Zenject;

namespace LostInSin.Raycast
{
    public class MousePositionRaycaster : IPositionRaycaster
    {
        [Inject] private MousePositionRayDrawer _mousePositionRayDrawer;
        private const int _groundLayer = 1 << 3;

        public bool GetWorldPosition(out Vector3 position)
        {
            position = default;

            Ray ray = _mousePositionRayDrawer.DrawRay();

            bool raycastSuccessful = false;

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _groundLayer))
            {
                position = hit.point;
                raycastSuccessful = true;
            }

            return raycastSuccessful;
        }


    }
}
