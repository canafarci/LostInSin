using System;
using LostInSin.Abilities;
using LostInSin.Characters.StateMachine;
using UnityEngine;
using Zenject;

namespace LostInSin.Characters
{
    /// <summary>
    /// Facade class for character
    /// </summary>
    public class Character : MonoBehaviour, IAbilityHolder
    {
        private IStateTicker _stateTicker;

        [Inject] private CharacterStateRuntimeData _runtimeData;
        [Inject] private AbilitySet _abilitySet;

        public AbilitySet AbilitySet { get; }
        public AttributeSet AttributeSet { get; }

        [Inject]
        private void Init(IStateTicker stateTicker, Transform inTransform, Vector3 position)
        {
            transform.position = position;
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