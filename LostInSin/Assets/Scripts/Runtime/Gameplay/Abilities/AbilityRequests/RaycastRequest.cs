using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityRequests
{
	public class RaycastRequest
	{
		public bool isProcessed { get; set; }

		private readonly Vector3 _mousePosition;

		public Vector3 mousePosition => _mousePosition;

		public RaycastRequest(Vector3 mousePosition)
		{
			_mousePosition = mousePosition;
		}
	}
}