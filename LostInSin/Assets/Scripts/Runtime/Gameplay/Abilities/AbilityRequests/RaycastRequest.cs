using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityRequests
{
	public class RaycastRequest
	{
		public bool isProcessed { get; private set; }

		private readonly Vector3 _mousePosition;

		public Vector3 mousePosition //when this property is accessed for raycasting, mark it as processed
		{
			get
			{
				isProcessed = true;
				return _mousePosition;
			}
		}

		public RaycastRequest(Vector3 mousePosition)
		{
			_mousePosition = mousePosition;
		}
	}
}