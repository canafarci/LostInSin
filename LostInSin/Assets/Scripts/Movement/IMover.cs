using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LostInSin.Movement
{
    public interface IMover
    {
        public void InitializeMovement(Vector3 target);
        public void Move();
        public bool HasReachedDestination();
        public bool MovementStarted { get; set; }
    }
}
