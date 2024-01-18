using System;
using System.Collections.Generic;
using LostInSin.Abilities;
using LostInSin.AbilitySystem;
using LostInSin.Characters.PersistentData;
using LostInSin.Characters.StateMachine;
using LostInSin.Movement;
using UnityEngine;
using Zenject;

namespace LostInSin.Characters
{
    /// <summary>
    /// Facade class for a character in the game.
    /// </summary>
    public class Character : MonoBehaviour, IAbilityHolder
    {
        private IStateTicker _stateTicker;

        [Inject] private CharacterStateRuntimeData _runtimeData;
        [Inject] private readonly AbilitySet _abilitySet;
        [Inject] private readonly AttributeSet _attributeSet;
        [Inject] private IMover _mover;
        [Inject] private SignalBus _signalBus;

        #region Getters

        public AbilitySet AbilitySet => _abilitySet;
        public AttributeSet AttributeSet => _attributeSet;
        public List<AbilityInfo> Abilities => _abilitySet.CharacterAbilities;
        public IMover Mover => _mover;
        public CharacterStateRuntimeData RuntimeData => _runtimeData;
        public SignalBus SignalBus => _signalBus;

        #endregion

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
            if (_runtimeData.CanExitState)
            {
                _runtimeData.IsTicking = false;
                _stateTicker.SwitchToInactiveState();
            }

            return _runtimeData.CanExitState;
        }

        public class Factory : PlaceholderFactory<Vector3, CharacterPersistentData, Character>
        {
        }
    }
}