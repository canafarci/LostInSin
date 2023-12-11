using System;
using LostInSin.Characters.StateMachine;
using UnityEngine;
using Zenject;

namespace LostInSin.Characters
{
    /// <summary>
    /// Facade class for character
    /// </summary>
    public class Character : MonoBehaviour
    {
        private Transform _transform;
        private IStateTicker _stateTicker;
        [Inject] private CharacterStateRuntimeData _runtimeData;

        [Inject]
        private void Init(IStateTicker stateTicker, Transform transform, Vector3 position)
        {
            _transform = transform;
            _transform.position = position;
            _stateTicker = stateTicker;
        }

        public void TickState()
        {
            _stateTicker.Tick();
        }

        public void SetAsTickingCharacter()
        {
            _runtimeData.IsTicking = true;
        }

        public bool CanExitTickingCharacter()
        {
            if (_runtimeData.CanExitTicking)
            {
                _runtimeData.IsTicking = false;
                _stateTicker.SwitchToInactiveState();
            }
            return _runtimeData.CanExitTicking;
        }

        public class Factory : PlaceholderFactory<Vector3, Character>
        {
        }
    }
}

