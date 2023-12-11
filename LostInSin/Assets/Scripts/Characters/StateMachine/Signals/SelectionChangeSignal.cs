using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LostInSin.Characters.StateMachine
{
    public class SelectionChangeSignal : ISelectionChangeSignal
    {
        private readonly bool _selected;
        public bool Selected { get { return _selected; } }
        public SelectionChangeSignal(bool selected) => _selected = selected;
    }
}
