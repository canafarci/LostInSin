using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LostInSin.Movement
{
    public interface IMover
    {
        public void InitializeMovement(Vector3 target);
        public bool Move();
        public bool IsMoving { get; }
    }
}