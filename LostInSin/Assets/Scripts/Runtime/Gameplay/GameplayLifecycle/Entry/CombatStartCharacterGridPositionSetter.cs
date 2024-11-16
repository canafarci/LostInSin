using System;
using LostInSin.Runtime.Gameplay.Characters;
using LostInSin.Runtime.Gameplay.Data.SceneReferences;
using LostInSin.Runtime.Grid;
using LostInSin.Runtime.Grid.Data;
using VContainer.Unity;

namespace LostInSin.Runtime.Gameplay.GameplayLifecycle.Entry
{
	public class CombatStartCharacterGridPositionSetter
	{
		private readonly ICharactersInSceneModel _currentCharactersInSceneModel;
		private readonly IGridPositionConverter _gridPositionConverter;

		public CombatStartCharacterGridPositionSetter(ICharactersInSceneModel currentCharactersInSceneModel,
			IGridPositionConverter gridPositionConverter)
		{
			_currentCharactersInSceneModel = currentCharactersInSceneModel;
			_gridPositionConverter = gridPositionConverter;
		}

		public void SetPositions()
		{
			foreach (CharacterFacade facade in _currentCharactersInSceneModel.allCharactersInScene)
			{
				if (_gridPositionConverter.GetCell(facade.transform.position, out GridCell cell))
				{
					facade.SetCharacterCell(cell);
				}
				else
				{
					throw new Exception("Grid position could not be found");
				}
			}
		}
	}
}