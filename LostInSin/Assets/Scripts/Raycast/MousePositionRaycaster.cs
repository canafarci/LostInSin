using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LostInSin.Raycast
{
    public class MousePositionRaycaster : IPositionRaycaster
    {
        public bool GetWorldPosition(out Vector3 position)
        {
            position = default;
            Vector2 screenPosition = GetScreenPosition();

            bool raycastSuccessful = false;
            Ray ray = Camera.main.ScreenPointToRay(screenPosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                position = hit.point;
                raycastSuccessful = true;
            }

            return raycastSuccessful;
        }

        private static Vector2 GetScreenPosition()
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector2 screenPosition = Camera.main.ScreenToViewportPoint(mousePosition);
            screenPosition.x *= Screen.width;
            screenPosition.y *= Screen.height;
            return screenPosition;
        }
    }
}
