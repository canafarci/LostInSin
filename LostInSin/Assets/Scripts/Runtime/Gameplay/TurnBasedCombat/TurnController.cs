using System.Collections.Generic;
using System.Linq;
using LostInSin.Runtime.Gameplay.Characters;
using LostInSin.Runtime.Gameplay.Data.SceneReferences;
using LostInSin.Runtime.Gameplay.GameplayLifecycle.Enums;
using LostInSin.Runtime.Gameplay.GameplayLifecycle.Signals;
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
			_signalBus.Subscribe<InitializeModulesSignal>(OnInitializeModulesSignalHandler);
			_signalBus.Subscribe<StartTurnBasedCombatSignal>(OnStartTurnBasedCombatSignalHandler);
			_signalBus.Subscribe<EndCharacterTurnSignal>(OnEndCharacterTurnSignalHandler);
			_signalBus.Subscribe<CharacterDiedSignal>(OnCharacterDiedSignalHandler);
			_mediator.OnEndTurnButtonClicked += OnEndTurnButtonClickedHandler;
		}

		private void OnEndCharacterTurnSignalHandler(EndCharacterTurnSignal signal) => AdvanceTurn();

		private void OnEndTurnButtonClickedHandler() => AdvanceTurn();

		private void OnCharacterDiedSignalHandler(CharacterDiedSignal signal)
		{
			_turnModel.characterTurnQueue.Remove(signal.character);
		}

		private void OnInitializeModulesSignalHandler(InitializeModulesSignal signal)
		{
			InitializeTurnOrder();
			_signalBus.Fire(new ModuleInitializedSignal(InitializableModule.TurnManager));
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
			_signalBus.Unsubscribe<InitializeModulesSignal>(OnInitializeModulesSignalHandler);
			_signalBus.Unsubscribe<StartTurnBasedCombatSignal>(OnStartTurnBasedCombatSignalHandler);
			_signalBus.Unsubscribe<EndCharacterTurnSignal>(OnEndCharacterTurnSignalHandler);
			_signalBus.Unsubscribe<CharacterDiedSignal>(OnCharacterDiedSignalHandler);
			_mediator.OnEndTurnButtonClicked -= OnEndTurnButtonClickedHandler;
		}
	}
}