using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace LostInSin.Input
{
    public class GameInput : IInitializable, IDisposable
    {
        private readonly PlayerInputActions _inputActions;
        private IObservable<InputAction.CallbackContext> _clickStream;
        readonly private CompositeDisposable _disposables = new();
        public IObservable<InputAction.CallbackContext> ClickStream { get { return _clickStream; } }

        private GameInput()
        {
            _inputActions = new PlayerInputActions();
        }

        public void Dispose()
        {
            //_clickStream.Dispose();
        }

        public void Initialize()
        {

            _inputActions.Enable();

            _clickStream = Observable.FromEvent<InputAction.CallbackContext>(
                h => _inputActions.Gameplay.Click.performed += h,
                h => _inputActions.Gameplay.Click.performed -= h
            );
        }
    }
}
