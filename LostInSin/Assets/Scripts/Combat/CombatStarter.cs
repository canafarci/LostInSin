using System;
using System.Collections.Generic;
using LostInSin.Characters;
using LostInSin.Grid;
using LostInSin.Grid.Visual;
using LostInSin.Signals.Combat;
using UniRx;
using UnityEngine;
using Zenject;

namespace LostInSin.Combat
{
    public class CombatStarter : IInitializable, IDisposable
    {
        [Inject] private SignalBus _signalBus;
        [Inject] private readonly GridGenerator _gridGenerator;
        [Inject] private readonly GridMeshDisplayService _gridDisplayer;
        [Inject] private readonly CombatCharacterPicker _combatCharacterPicker;
        [Inject] private readonly TurnManager _turnManager;

        private CompositeDisposable _disposables = new();

        public void Initialize()
        {
            _signalBus.GetStream<CombatStartedSignal>()
                      .Subscribe(OnCombatStarted)
                      .AddTo(_disposables);
        }

        private void OnCombatStarted(CombatStartedSignal signal)
        {
            _gridGenerator.GenerateGrid();
            _gridDisplayer.ShowGrid();

            List<Character> characters = _combatCharacterPicker.GetCombatCharacters();
            _turnManager.InitializeTurns(characters);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}