using LostInSin.Runtime.Gameplay.Data.SceneReferences;
using LostInSin.Runtime.Gameplay.Turns;
using LostInSin.Runtime.Grid;
using LostInSin.Runtime.Infrastructure;
using LostInSin.Runtime.Pathfinding;
using UnityEngine;
using VContainer;

namespace LostInSin.Runtime.BehaviourTree
{
	public class BTReferences : MonoSingleton<BTReferences>
	{
		[Inject] private TurnSystemFacade _turnSystemFacade;
		[Inject] private ICharactersInSceneModel _charactersInSceneModel;
		[Inject] private IGridPositionConverter _gridPositionConverter;
		[Inject] private IGridPathfinder _gridPathfinder;

		public TurnSystemFacade turnSystemFacade => _turnSystemFacade;
		public IGridPathfinder gridPathfinder => _gridPathfinder;
		public ICharactersInSceneModel charactersInSceneModel => _charactersInSceneModel;
		public IGridPositionConverter gridPositionConverter => _gridPositionConverter;
	}
}