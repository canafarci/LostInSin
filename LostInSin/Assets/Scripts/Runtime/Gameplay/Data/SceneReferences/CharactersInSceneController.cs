using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Characters;
using VContainer;
using VContainer.Unity;

namespace LostInSin.Runtime.Gameplay.Data.SceneReferences
{
	public class CharactersInSceneController : IInitializable
	{
		[Inject] private List<CharacterFacade> _enemyCharactersInScene;
		[Inject] ICharactersInSceneModel _charactersInSceneModel;

		public void Initialize()
		{
			_charactersInSceneModel.enemyCharactersInScene.AddRange(_enemyCharactersInScene);
		}
	}
}