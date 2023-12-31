using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace LostInSin.Camera
{
    public class CameraModel
    {
        private CinemachineComposer _cameraComposer;
        private GameObject _cameraTarget;
        private CinemachineTransposer _cameraTransposer;
        private readonly CameraData _cameraData;
        private readonly CinemachineVirtualCamera _camera;

        public float CameraMoveSpeed { get { return _cameraData.CameraDataSO.CameraMoveSpeed; } }
        public float CameraZoomMinDistance { get { return _cameraData.CameraDataSO.CameraZoomMinDistance; } }
        public float CameraZoomMaxDistance { get { return _cameraData.CameraDataSO.CameraZoomMaxDistance; } }
        public float CameraZoomSpeed { get { return _cameraData.CameraDataSO.CameraZoomSpeed; } }
        public CinemachineTransposer CameraTransposer { get { return _cameraTransposer; } }
        public CinemachineComposer CameraComposer { get { return _cameraComposer; } }
        public GameObject CameraTarget { get { return _cameraTarget; } }
        public CinemachineVirtualCamera Camera { get { return _camera; } }


        private CameraModel(CinemachineVirtualCamera camera, CameraData cameraData)
        {
            _camera = camera;
            _cameraData = cameraData;
        }

        internal void SetCameraReferences(CinemachineTransposer cameraTransposer,
                                             CinemachineComposer cameraComposer,
                                             GameObject cameraTarget)
        {
            _cameraTransposer = cameraTransposer;
            _cameraComposer = cameraComposer;
            _cameraTarget = cameraTarget;
        }

        public class CameraData
        {
            public CameraDataSO CameraDataSO;
        }
    }
}
