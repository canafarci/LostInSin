using System;
using UnityEngine;
using Zenject;


namespace LostInSin.Raycast
{
    public class ComponentRaycaster<T> : IComponentRaycaster<T> where T : Component
    {
        [Inject] private MousePositionRayDrawer _mousePositionRayDrawer;

        public bool RaycastCharacter(out T component, int layerMask)
        {
            component = default;

            Ray ray = _mousePositionRayDrawer.DrawRay();

            bool raycastSuccessful = false;

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
            {
                Transform transform = hit.transform;
                component = transform.GetComponent<T>();

                if (component != null)
                {
                    raycastSuccessful = true;
                }
                else
                {
                    throw new Exception($"No component found of type {typeof(T)} found on GameObject {transform.gameObject}!");
                }
            }

            return raycastSuccessful;
        }
    }
}
