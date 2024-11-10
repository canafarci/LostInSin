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
		[Inject] private ICharactersInSceneModel _charactersInSceneModel;
		private Queue<CharacterFacade> _characterTurnQueue = new();

		protected override void SubscribeToEvents()
		{
			_signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChangedHandler);
		}

		private void OnGameStateChangedHandler(GameStateChangedSignal signal)
		{
			if (signal.newState == GameState.Playing)
			{
				InitializeTurnOrder();
				ProcessTurn();
			}
		}

		private void InitializeTurnOrder()
		{
			List<CharacterFacade> allCharacters =
				new List<CharacterFacade>(_charactersInSceneModel.allCharactersInScene)
					.OrderBy(facade => facade.character.Initiative)
					.ToList();

			foreach (CharacterFacade facade in allCharacters)
			{
				_characterTurnQueue.Enqueue(facade);
			}
		}

		private void ProcessTurn()
		{
			CharacterFacade characterToPlay = _characterTurnQueue.Dequeue();
			//push character to the end of the queue
			_characterTurnQueue.Enqueue(characterToPlay);

			characterToPlay.SetAsActiveCharacter();
		}

		protected override void UnsubscribeFromEvents()
		{
			_signalBus.Unsubscribe<GameStateChangedSignal>(OnGameStateChangedHandler);
		}
	}
}