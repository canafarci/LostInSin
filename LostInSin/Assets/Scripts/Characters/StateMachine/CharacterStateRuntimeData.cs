using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LostInSin.Characters
{
    public class CharacterStateRuntimeData
    {
        private bool _isTicking = false;
        private bool _canExitTicking = true;
        public bool IsTicking { get { return _isTicking; } set { _isTicking = value; } }
        public bool CanExitTicking { get { return _canExitTicking; } set { _canExitTicking = value; } }
    }
}
