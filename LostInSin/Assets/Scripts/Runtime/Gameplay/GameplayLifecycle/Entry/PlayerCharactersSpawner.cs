using UnityEngine;
using VContainer;
using LostInSin.Runtime.Gameplay.Characters;
using LostInSin.Runtime.Gameplay.Data;
using LostInSin.Runtime.Gameplay.Data.SceneReferences;
using LostInSin.Runtime.Gameplay.GameplayLifecycle.Enums;
using LostInSin.Runtime.Gameplay.GameplayLifecycle.Signals;
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
			_signalBus.Subscribe<InitializeModulesSignal>(OnInitializeModulesSignalHandler);
		}

		private void OnInitializeModulesSignalHandler(InitializeModulesSignal signal)
		{
			for (int i = 0; i < _playerCharactersSO.PlayerCharacters.Count; i++)
			{
				CharacterData characterData = _playerCharactersSO.PlayerCharacters[i];
				GameObject character = Object.Instantiate(characterData.Prefab);
				character.transform.position += Vector3.left * 2f * i;
				_charactersInSceneModel.playerCharactersInScene.Add(character.GetComponent<CharacterFacade>());
			}

			_signalBus.Fire(new ModuleInitializedSignal(InitializableModule.PlayerCharacterSpawner));
		}

		protected override void UnsubscribeFromEvents()
		{
			_signalBus.Unsubscribe<InitializeModulesSignal>(OnInitializeModulesSignalHandler);
		}
	}
}