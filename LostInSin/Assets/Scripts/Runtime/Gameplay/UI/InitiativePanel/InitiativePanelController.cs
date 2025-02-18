using System;
using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Characters;
using LostInSin.Runtime.Gameplay.Signals;
using LostInSin.Runtime.Gameplay.TurnBasedCombat;
using LostInSin.Runtime.Infrastructure.Templates;
using R3;
using UnityEngine;
using UnityEngine.Assertions;
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
			_signalBus.Subscribe<CharacterDiedSignal>(OnCharacterDiedSignalHandler);
			_signalBus.Subscribe<UpdateInitiativePanelSignal>(OnUpdateInitiativePanelSignalHandler);
		}

		private void OnActiveTurnCharacterChangedSignalHandler(ActiveTurnCharacterChangedSignal signal) => UpdatePanelIcons();

		private void OnStartTurnBasedCombatSignalHandler(StartTurnBasedCombatSignal signal) => UpdatePanelIcons();

		private void OnCharacterDiedSignalHandler(CharacterDiedSignal signal) => UpdatePanelIcons();

		private void OnUpdateInitiativePanelSignalHandler(UpdateInitiativePanelSignal signal) => UpdatePanelIcons();

		private void UpdatePanelIcons()
		{
			LinkedList<CharacterFacade> turnQueue = _turnModel.characterTurnQueue;

			LinkedListNode<CharacterFacade> characterNode = turnQueue.First;

			Assert.IsNotNull(characterNode, "First character node is null! Check Turn Order linked list!");

			foreach (InitiativeIconView view in _initiativePanelView.initiativeIcons)
			{
				view.icon.sprite = characterNode.Value.visualReferences.characterPortrait;
				view.healthBarFillImage.fillAmount = characterNode.Value.healthPercentage;

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
			_signalBus.Unsubscribe<StartTurnBasedCombatSignal>(OnStartTurnBasedCombatSignalHandler);
			_signalBus.Unsubscribe<ActiveTurnCharacterChangedSignal>(OnActiveTurnCharacterChangedSignalHandler);
			_signalBus.Unsubscribe<CharacterDiedSignal>(OnCharacterDiedSignalHandler);
			_signalBus.Unsubscribe<UpdateInitiativePanelSignal>(OnUpdateInitiativePanelSignalHandler);
		}
	}
}