using LostInSin.Runtime.Gameplay.Turns;
using LostInSin.Runtime.Infrastructure;
using UnityEngine;
using VContainer;

namespace LostInSin.Runtime.BehaviourTree
{
	public class BTReferences : MonoSingleton<BTReferences>
	{
		[Inject] private TurnSystemFacade _turnSystemFacade;

		public TurnSystemFacade turnSystemFacade => _turnSystemFacade;
	}
}