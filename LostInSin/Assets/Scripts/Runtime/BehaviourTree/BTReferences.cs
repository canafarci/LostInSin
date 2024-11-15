using LostInSin.Runtime.Gameplay.Turns;
using UnityEngine;
using VContainer;

namespace LostInSin.Runtime.BehaviourTree
{
	public class BTReferences : MonoBehaviour
	{
		[Inject] private TurnSystemFacade _turnSystemFacade;

		public TurnSystemFacade turnSystemFacade => _turnSystemFacade;
	}
}