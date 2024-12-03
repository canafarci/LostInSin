using LostInSin.Runtime.Gameplay.Data.SceneReferences;
using LostInSin.Runtime.Gameplay.Grid;
using LostInSin.Runtime.Gameplay.Pathfinding;
using LostInSin.Runtime.Gameplay.TurnBasedCombat;
using LostInSin.Runtime.Infrastructure;
using LostInSin.Runtime.Infrastructure.Signals;
using VContainer;

namespace LostInSin.Runtime.Gameplay.BehaviourTree.Utility
{
	public class BTReferences : MonoSingleton<BTReferences>
	{
		[Inject] private TurnSystemFacade _turnSystemFacade;
		[Inject] private ICharactersInSceneModel _charactersInSceneModel;
		[Inject] private IGridPositionConverter _gridPositionConverter;
		[Inject] private IGridPathfinder _gridPathfinder;
		[Inject] private SignalBus _signalBus;

		public TurnSystemFacade turnSystemFacade => _turnSystemFacade;
		public IGridPathfinder gridPathfinder => _gridPathfinder;
		public ICharactersInSceneModel charactersInSceneModel => _charactersInSceneModel;
		public IGridPositionConverter gridPositionConverter => _gridPositionConverter;
		public SignalBus signalBus => _signalBus;
	}
}