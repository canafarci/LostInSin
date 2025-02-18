using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Visuals.UI
{
	public class WorldCanvasFaceCamera : MonoBehaviour
	{
		private Camera _targetCamera;

		private void Awake()
		{
			// If no camera is assigned, use the main camera.
			if (_targetCamera == null)
			{
				_targetCamera = Camera.main;
			}
		}

		private void LateUpdate()
		{
			if (_targetCamera != null)
			{
				transform.rotation = Quaternion.LookRotation(_targetCamera.transform.forward, _targetCamera.transform.up);
			}
		}
	}
}