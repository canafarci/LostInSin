using System;
using System.Collections.Generic;
using LostInSin.Abilities;
using LostInSin.AbilitySystem;
using LostInSin.Animation;
using LostInSin.Characters.PersistentData;
using LostInSin.Characters.StateMachine;
using LostInSin.Identifiers;
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

        [Inject] private readonly CharacterStateRuntimeData _runtimeData;
        [Inject] private readonly AbilitySet _abilitySet;
        [Inject] private readonly AttributeSet _attributeSet;
        [Inject] private readonly IMover _mover;
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly AnimationReference _animationReference;
        [Inject] private readonly CharacterPersistentData _persistentData;

        #region Getters

        public AbilitySet AbilitySet => _abilitySet;
        public AttributeSet AttributeSet => _attributeSet;
        public List<AbilityInfo> Abilities => _abilitySet.CharacterAbilities;
        public IMover Mover => _mover;
        public CharacterStateRuntimeData RuntimeData => _runtimeData;
        public SignalBus SignalBus => _signalBus;
        public AnimationReference AnimationReference => _animationReference;
        public CharacterPersistentData CharacterPersistentData => _persistentData;

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

        public bool CanExitTickingCharacter() => _runtimeData.CanExitState;

        public void ExitTickingCharacter()
        {
            _runtimeData.IsTicking = false;
            _stateTicker.SwitchToInactiveState();
        }

        public class Factory : PlaceholderFactory<Vector3, CharacterPersistentData, Character>
        {
        }
    }
}