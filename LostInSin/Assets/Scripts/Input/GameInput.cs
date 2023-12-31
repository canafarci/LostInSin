using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace LostInSin.Input
{
    public class GameInput : IInitializable
    {
        private readonly PlayerInputActions _inputActions;
        public PlayerInputActions.GameplayActions GameplayActions { get { return _gameplayActions; } }
        public PlayerInputActions.GameplayActions _gameplayActions;

        public GameInput()
        {
            _inputActions = new PlayerInputActions();
        }

        public void Initialize()
        {
            _inputActions.Enable();
            _gameplayActions = _inputActions.Gameplay;
        }
    }
}
