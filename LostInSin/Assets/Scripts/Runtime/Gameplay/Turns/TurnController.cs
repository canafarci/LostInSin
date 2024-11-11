using System.Collections.Generic;
using System.Linq;
using LostInSin.Runtime.Gameplay.Characters;
using LostInSin.Runtime.Gameplay.Data.SceneReferences;
using LostInSin.Runtime.Gameplay.Enums;
using LostInSin.Runtime.Gameplay.Signals;
using LostInSin.Runtime.Infrastructure.Templates;
using VContainer;

namespace LostInSin.Runtime.Gameplay.Turns
{
	public class TurnController : SignalListener
	{
		private readonly ICharactersInSceneModel _charactersInSceneModel;
		private readonly ITurnModel _turnModel;
		private readonly TurnView _view;


		private readonly Queue<CharacterFacade> _characterTurnQueue = new();

		public TurnController(ICharactersInSceneModel charactersInSceneModel, TurnView view, ITurnModel turnModel)
		{
			_charactersInSceneModel = charactersInSceneModel;
			_view = view;
			_turnModel = turnModel;
		}

		protected override void SubscribeToEvents()
		{
			_signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChangedHandler);
			_view.endTurnButton.onClick.AddListener(OnEndTurnButtonClickedHandler);
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
					.OrderBy(facade => facade.character.initiative)
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
			_turnModel.characterToPlay = characterToPlay;

			characterToPlay.SetAsActiveCharacter();
		}

		protected override void UnsubscribeFromEvents()
		{
			_signalBus.Unsubscribe<GameStateChangedSignal>(OnGameStateChangedHandler);
			_view.endTurnButton.onClick.RemoveAllListeners();
		}
	}
}