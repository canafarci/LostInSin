using System.Collections.Generic;
using System.Linq;
using LostInSin.Runtime.Gameplay.Characters;
using LostInSin.Runtime.Gameplay.Data.SceneReferences;
using LostInSin.Runtime.Gameplay.Enums;
using LostInSin.Runtime.Gameplay.Signals;
using LostInSin.Runtime.Gameplay.UI.Turns;
using LostInSin.Runtime.Infrastructure.Templates;
using VContainer;

namespace LostInSin.Runtime.Gameplay.Turns
{
	public class TurnController : SignalListener
	{
		private readonly ICharactersInSceneModel _charactersInSceneModel;
		private readonly TurnMediator _mediator;
		private readonly ITurnModel _turnModel;


		private readonly Queue<CharacterFacade> _characterTurnQueue = new();

		public TurnController(ICharactersInSceneModel charactersInSceneModel, TurnMediator mediator,
			ITurnModel turnModel)
		{
			_charactersInSceneModel = charactersInSceneModel;
			_mediator = mediator;
			_turnModel = turnModel;
		}

		protected override void SubscribeToEvents()
		{
			_signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChangedHandler);
			_signalBus.Subscribe<EndCharacterTurnSignal>(OnEndCharacterTurnSignal);
			_mediator.OnEndTurnButtonClicked += OnEndTurnButtonClickedHandler;
		}

		private void OnEndCharacterTurnSignal(EndCharacterTurnSignal signal)
		{
			AdvanceTurn();
		}

		private void OnEndTurnButtonClickedHandler()
		{
			AdvanceTurn();
		}

		private void OnGameStateChangedHandler(GameStateChangedSignal signal)
		{
			if (signal.newState == GameState.Playing)
			{
				InitializeTurnOrder();
				AdvanceTurn();
			}
		}

		private void InitializeTurnOrder()
		{
			List<CharacterFacade> allCharacters =
				new List<CharacterFacade>(_charactersInSceneModel.allCharactersInScene)
					.OrderBy(facade => facade.initiative)
					.ToList();

			foreach (CharacterFacade facade in allCharacters)
			{
				_characterTurnQueue.Enqueue(facade);
			}
		}

		private void AdvanceTurn()
		{
			CharacterFacade characterToPlay = _characterTurnQueue.Dequeue();
			//push character to the end of the queue
			_characterTurnQueue.Enqueue(characterToPlay);
			_turnModel.activeCharacter = characterToPlay;
			_mediator.SetUpUI(characterToPlay);

			characterToPlay.SetAsActiveCharacter();
			_signalBus.Fire(new ActiveTurnCharacterChangedSignal());
		}

		protected override void UnsubscribeFromEvents()
		{
			_signalBus.Unsubscribe<GameStateChangedSignal>(OnGameStateChangedHandler);
			_signalBus.Unsubscribe<EndCharacterTurnSignal>(OnEndCharacterTurnSignal);
			_mediator.OnEndTurnButtonClicked -= OnEndTurnButtonClickedHandler;
		}
	}
}