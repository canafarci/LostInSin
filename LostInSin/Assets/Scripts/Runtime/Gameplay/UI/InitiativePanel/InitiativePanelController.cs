using System;
using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Characters;
using LostInSin.Runtime.Gameplay.Signals;
using LostInSin.Runtime.Gameplay.TurnBasedCombat;
using LostInSin.Runtime.Infrastructure.Templates;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace LostInSin.Runtime.Gameplay.UI.InitiativePanel
{
	public class InitiativePanelController : SignalListener
	{
		[Inject] private ITurnModel _turnModel;

		protected override void SubscribeToEvents()
		{
			_signalBus.Subscribe<StartTurnBasedCombatSignal>(OnStartTurnBasedCombatSignalHandler);
		}

		private void OnStartTurnBasedCombatSignalHandler(StartTurnBasedCombatSignal signal)
		{
			Debug.Log("Setting Up Initiative Panel");
		}

		protected override void UnsubscribeFromEvents()
		{
			_signalBus.Subscribe<StartTurnBasedCombatSignal>(OnStartTurnBasedCombatSignalHandler);
		}
	}
}