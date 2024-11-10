using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Characters;

namespace LostInSin.Runtime.Gameplay.Data.SceneReferences
{
	public interface ICharactersInSceneModel
	{
		public List<CharacterFacade> enemyCharactersInScene { get; set; }
		public List<CharacterFacade> playerCharactersInScene { get; set; }
		public List<CharacterFacade> allCharactersInScene { get; }
	}
}