using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Cameras.Data
{
	[CreateAssetMenu(fileName = "Camera Config", menuName = "Infrastructure/Camera Config", order = 0)]
	public class CameraConfig : ScriptableObject
	{
		[SerializeField] private float CameraSpeed;

		public float cameraSpeed => CameraSpeed;
	}
}