using LostInSin.Runtime.CrossScene.Data;
using LostInSin.Runtime.CrossScene.Signals;
using LostInSin.Runtime.Infrastructure.ApplicationState;
using LostInSin.Runtime.Infrastructure.ApplicationState.Signals;
using LostInSin.Runtime.Infrastructure.Data;
using LostInSin.Runtime.Infrastructure.Signals;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace LostInSin.Runtime.Bootstrap
{
	public class BootstrapSceneEntryPoint : IInitializable
	{
		[Inject] private SignalBus _signalBus;
		[Inject] private IGameplayPersistentData _gameplayPersistentData;
		[Inject] private ApplicationSettings _applicationSettings;
		[Inject] private AppInitializer _appInitializer;

		public void Initialize()
		{
			_signalBus.Fire(new ChangeAppStateSignal(AppStateID.Initializing));

			_appInitializer.ApplyAppSettings();
			LoadNextScene();
		}

		private void LoadNextScene()
		{
#if UNITY_EDITOR
			if (LoadSceneAfterBootstrap()) return;
#endif

			var sceneIndex = _applicationSettings.HasMainMenu
				? _applicationSettings.MainMenuSceneIndex
				: _gameplayPersistentData.levelToLoadIndex;

			_signalBus.Fire(new LoadSceneSignal(sceneIndex));
		}

#if UNITY_EDITOR
		private bool LoadSceneAfterBootstrap()
		{
			const string sceneToLoadAfterBootstrapKey = "SceneToLoadAfterBootstrap";
			if (UnityEditor.EditorPrefs.HasKey(sceneToLoadAfterBootstrapKey))
			{
				var sceneToLoadPath = UnityEditor.EditorPrefs.GetString(sceneToLoadAfterBootstrapKey);
				// Clean up the key after use
				UnityEditor.EditorPrefs.DeleteKey(sceneToLoadAfterBootstrapKey);

				if (!string.IsNullOrEmpty(sceneToLoadPath))
				{
					// Load the scene by path
					SceneManager.LoadScene(sceneToLoadPath, LoadSceneMode.Single);
					return true;
				}
			}

			return false;
		}
#endif
	}
}