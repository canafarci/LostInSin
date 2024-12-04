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
		private readonly ITurnModel _turnModel;
		private readonly InitiativePanelView _initiativePanelView;

		public InitiativePanelController(ITurnModel turnModel, InitiativePanelView initiativePanelView)
		{
			_turnModel = turnModel;
			_initiativePanelView = initiativePanelView;
		}

		protected override void SubscribeToEvents()
		{
			_signalBus.Subscribe<StartTurnBasedCombatSignal>(OnStartTurnBasedCombatSignalHandler);
			_signalBus.Subscribe<ActiveTurnCharacterChangedSignal>(OnActiveTurnCharacterChangedSignalHandler);
		}

		private void OnActiveTurnCharacterChangedSignalHandler(ActiveTurnCharacterChangedSignal signal) => UpdatePanelIcons();

		private void OnStartTurnBasedCombatSignalHandler(StartTurnBasedCombatSignal signal) => UpdatePanelIcons();

		private void UpdatePanelIcons()
		{
			LinkedList<CharacterFacade> turnQueue = _turnModel.characterTurnQueue;

			LinkedListNode<CharacterFacade> characterNode = turnQueue.First;

			foreach (InitiativeIconView view in _initiativePanelView.initiativeIcons)
			{
				view.icon.sprite = characterNode.Value.visualReferences.characterPortrait;

				if (characterNode == turnQueue.Last)
				{
					characterNode = turnQueue.First;
				}
				else
				{
					characterNode = characterNode.Next;
				}
			}
		}

		protected override void UnsubscribeFromEvents()
		{
			_signalBus.Subscribe<StartTurnBasedCombatSignal>(OnStartTurnBasedCombatSignalHandler);
			_signalBus.Unsubscribe<ActiveTurnCharacterChangedSignal>(OnActiveTurnCharacterChangedSignalHandler);
		}
	}
}