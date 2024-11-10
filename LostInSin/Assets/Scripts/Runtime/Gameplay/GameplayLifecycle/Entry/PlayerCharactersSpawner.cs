using UnityEngine;
using VContainer;
using LostInSin.Runtime.Gameplay.Characters;
using LostInSin.Runtime.Gameplay.Data;
using LostInSin.Runtime.Gameplay.Data.SceneReferences;
using LostInSin.Runtime.Gameplay.Enums;
using LostInSin.Runtime.Gameplay.Signals;
using LostInSin.Runtime.Infrastructure.Templates;

namespace LostInSin.Runtime.Gameplay.GameplayLifecycle.Entry
{
	public class PlayerCharactersSpawner : SignalListener
	{
		[Inject] private PlayerCharactersSO _playerCharactersSO;
		[Inject] private ICharactersInSceneModel _charactersInSceneModel;

		protected override void SubscribeToEvents()
		{
			_signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChangedHandler);
		}

		private void OnGameStateChangedHandler(GameStateChangedSignal signal)
		{
			if (signal.newState == GameState.Initializing)
			{
				foreach (CharacterData characterData in _playerCharactersSO.PlayerCharacters)
				{
					GameObject character = Object.Instantiate(characterData.Prefab);
					_charactersInSceneModel.playerCharactersInScene.Add(character.GetComponent<CharacterFacade>());
				}
			}
		}

		protected override void UnsubscribeFromEvents()
		{
			_signalBus.Unsubscribe<GameStateChangedSignal>(OnGameStateChangedHandler);
		}
	}
}