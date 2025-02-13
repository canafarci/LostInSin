using LostInSin.Runtime.Gameplay.Cameras.Data;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace LostInSin.Runtime.Gameplay.Cameras
{
	public class CameraController : IStartable, ITickable
	{
		private readonly CameraView _view;
		private readonly CameraConfig _config;
		private InputAction _moveCameraAction;

		public void Start()
		{
			_moveCameraAction = InputSystem.actions.FindAction("MoveCamera");
		}

		public CameraController(CameraView view, CameraConfig config)
		{
			_view = view;
			_config = config;
		}

		public void Tick()
		{
			Vector2 moveInput = _moveCameraAction.ReadValue<Vector2>();
			_view.transform.position += new Vector3(moveInput.x, 0f, moveInput.y) * Time.deltaTime * _config.cameraSpeed;
		}
	}
}