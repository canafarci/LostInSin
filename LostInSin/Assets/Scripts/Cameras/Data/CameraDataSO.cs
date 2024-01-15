using Sirenix.OdinInspector;
using UnityEngine;

namespace LostInSin.Cameras.Data
{
    [CreateAssetMenu(fileName = "CameraDataSO", menuName = "LostInSin/CameraData", order = 0)]
    public class CameraDataSO : SerializedScriptableObject
    {
        public float CameraMoveSpeed;
        public float CameraZoomSpeed;
        public float CameraZoomMinDistance;
        public float CameraZoomMaxDistance;
        public float CameraRotateSpeed;
    }
}