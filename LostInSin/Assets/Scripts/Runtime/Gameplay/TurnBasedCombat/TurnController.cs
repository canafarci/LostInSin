using System.Collections.Generic;
using System.Linq;
using LostInSin.Runtime.Gameplay.Characters;
using LostInSin.Runtime.Gameplay.Data.SceneReferences;
using LostInSin.Runtime.Gameplay.Enums;
using LostInSin.Runtime.Gameplay.Signals;
using LostInSin.Runtime.Gameplay.UI.Turns;
using LostInSin.Runtime.Infrastructure.Templates;

namespace LostInSin.Runtime.Gameplay.TurnBasedCombat
{
	public class TurnController : SignalListener
	{
		private readonly ICharactersInSceneModel _charactersInSceneModel;
		private readonly TurnMediator _mediator;
		private readonly ITurnModel _turnModel;

		public TurnController(ICharactersInSceneModel charactersInSceneModel,
			TurnMediator mediator,
			ITurnModel turnModel)
		{
			_charactersInSceneModel = charactersInSceneModel;
			_mediator = mediator;
			_turnModel = turnModel;
		}

		protected override void SubscribeToEvents()
		{
			_signalBus.Subscribe<InitializeTurnBasedCombatSignal>(OnInitializeTurnBasedCombatSignalHandler);
			_signalBus.Subscribe<StartTurnBasedCombatSignal>(OnStartTurnBasedCombatSignalHandler);
			_signalBus.Subscribe<EndCharacterTurnSignal>(OnEndCharacterTurnSignal);
			_mediator.OnEndTurnButtonClicked += OnEndTurnButtonClickedHandler;
		}

		private void OnEndCharacterTurnSignal(EndCharacterTurnSignal signal) => AdvanceTurn();

		private void OnEndTurnButtonClickedHandler() => AdvanceTurn();

		private void OnInitializeTurnBasedCombatSignalHandler(InitializeTurnBasedCombatSignal signal)
		{
			InitializeTurnOrder();
		}

		private void OnStartTurnBasedCombatSignalHandler(StartTurnBasedCombatSignal signal)
		{
			SetCharacterTurn(_turnModel.characterTurnQueue.First.Value);
		}

		private void InitializeTurnOrder()
		{
			List<CharacterFacade> allCharacters =
				new List<CharacterFacade>(_charactersInSceneModel.allCharactersInScene)
					.OrderBy(facade => facade.initiative)
					.ToList();

			foreach (CharacterFacade facade in allCharacters)
			{
				_turnModel.characterTurnQueue.AddFirst(facade);
			}
		}

		private void AdvanceTurn()
		{
			//push character to the end of the linked list
			LinkedListNode<CharacterFacade> firstNode = _turnModel.characterTurnQueue.First;

			_turnModel.characterTurnQueue.RemoveFirst();
			_turnModel.characterTurnQueue.AddLast(firstNode);

			CharacterFacade characterToPlay = _turnModel.characterTurnQueue.First.Value;

			SetCharacterTurn(characterToPlay);
		}

		private void SetCharacterTurn(CharacterFacade characterToPlay)
		{
			_turnModel.activeCharacter = characterToPlay;
			_mediator.SetUpUI(characterToPlay);

			characterToPlay.SetAsActiveCharacter();
			_signalBus.Fire(new ActiveTurnCharacterChangedSignal());
		}

		protected override void UnsubscribeFromEvents()
		{
			_signalBus.Unsubscribe<InitializeTurnBasedCombatSignal>(OnInitializeTurnBasedCombatSignalHandler);
			_signalBus.Unsubscribe<StartTurnBasedCombatSignal>(OnStartTurnBasedCombatSignalHandler);
			_signalBus.Unsubscribe<EndCharacterTurnSignal>(OnEndCharacterTurnSignal);
			_mediator.OnEndTurnButtonClicked -= OnEndTurnButtonClickedHandler;
		}
	}
}