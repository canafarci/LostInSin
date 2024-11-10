using System.Collections.Generic;
using System.Linq;
using LostInSin.Runtime.Gameplay.Characters;

namespace LostInSin.Runtime.Gameplay.Data.SceneReferences
{
	public class CharactersInSceneModel : ICharactersInSceneModel
	{
		public List<CharacterFacade> enemyCharactersInScene { get; set; } = new();
		public List<CharacterFacade> playerCharactersInScene { get; set; } = new();

		public List<CharacterFacade> allCharactersInScene => new List<CharacterFacade>(enemyCharactersInScene)
			.Concat(playerCharactersInScene).ToList();
	}
}