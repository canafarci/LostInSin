using UnityEngine;
using UnityEngine.InputSystem;

namespace LostInSin.Raycast
{
    public class MousePositionRayDrawer : IRayDrawer
    {
        public Ray DrawRay()
        {
            Vector2 screenPosition = GetScreenPosition();

            Ray ray = Camera.main.ScreenPointToRay(screenPosition);
            return ray;
        }

        private Vector2 GetScreenPosition()
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector2 screenPosition = Camera.main.ScreenToViewportPoint(mousePosition);
            screenPosition.x *= Screen.width;
            screenPosition.y *= Screen.height;
            return screenPosition;
        }
    }
}
