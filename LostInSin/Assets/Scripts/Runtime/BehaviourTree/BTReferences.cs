using LostInSin.Runtime.Gameplay.Turns;
using UnityEngine;
using VContainer;

namespace LostInSin.Runtime.BehaviourTree
{
	public class BTReferences : MonoBehaviour
	{
		[Inject] private ITurnModel _turnModel;

		public ITurnModel TurnModel => _turnModel;
	}
}