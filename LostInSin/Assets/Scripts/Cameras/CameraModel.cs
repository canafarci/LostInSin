using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using LostInSin.Cameras.Data;
using UnityEngine;

namespace LostInSin.Cameras
{
    public class CameraModel
    {
        private CinemachineComposer _cameraComposer;
        private GameObject _cameraTarget;
        private CinemachineTransposer _cameraTransposer;
        private readonly CameraData _cameraData;
        private readonly CinemachineVirtualCamera _camera;

        public float CameraMoveSpeed => _cameraData.CameraDataSO.CameraMoveSpeed;
        public float CameraZoomMinDistance => _cameraData.CameraDataSO.CameraZoomMinDistance;
        public float CameraZoomMaxDistance => _cameraData.CameraDataSO.CameraZoomMaxDistance;
        public float CameraZoomSpeed => _cameraData.CameraDataSO.CameraZoomSpeed;
        public float CameraRotateSpeed => _cameraData.CameraDataSO.CameraRotateSpeed;

        public CinemachineTransposer CameraTransposer => _cameraTransposer;
        public CinemachineComposer CameraComposer => _cameraComposer;
        public CinemachineVirtualCamera Camera => _camera;
        public GameObject CameraTarget => _cameraTarget;


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