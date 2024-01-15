
using Cinemachine;
using UnityEngine;
using Zenject;

namespace LostInSin.Cameras
{
    public class CameraInitializer : IInitializable
    {
        private CinemachineVirtualCamera _camera;
        private CameraModel _cameraModel;

        private GameObject _cameraTarget;

        private CameraInitializer(CinemachineVirtualCamera camera,
                                CameraModel cameraModel)
        {
            _camera = camera;
            _cameraModel = cameraModel;

        }

        public void Initialize()
        {
            CreateCameraTarget();
            SetCameraFollowAndLookAt();
            SetCameratComponents();
        }


        private void CreateCameraTarget()
        {
            _cameraTarget = new GameObject("Camera Target");
            _cameraTarget.transform.SetParent(_camera.transform.parent);
        }

        private void SetCameraFollowAndLookAt()
        {
            _camera.Follow = _cameraTarget.transform;
            _camera.LookAt = _cameraTarget.transform;
        }

        private void SetCameratComponents()
        {
            CinemachineTransposer cameraTransposer = _camera.GetCinemachineComponent<CinemachineTransposer>();
            CinemachineComposer cameraComposer = _camera.GetCinemachineComponent<CinemachineComposer>();
            _cameraModel.SetCameraReferences(cameraTransposer, cameraComposer, _cameraTarget);
        }
    }
}
