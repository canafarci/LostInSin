using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LostInSin.Raycast
{
    public interface IPositionRaycaster
    {
        public bool GetWorldPosition(out Vector3 position);
    }
}
