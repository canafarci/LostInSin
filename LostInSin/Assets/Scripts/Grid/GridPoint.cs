using UnityEngine;

namespace LostInSin.Grid
{
    public struct GridPoint
    {
        public float PosX { get { return _posX; } }
        public float PosY { get { return _posY; } }
        public float PosZ { get { return _posZ; } }
        public bool IsVoid { get { return _isVoid; } }
        private float _posX;
        private float _posY;
        private float _posZ;
        private bool _isVoid;

        public GridPoint(float posX, float posY, float posZ, bool isVoid = true)
        {
            _posX = posX;
            _posY = posY;
            _posZ = posZ;
            _isVoid = isVoid;
        }

        public readonly Vector3 ToVector3() => new Vector3(_posX, _posY, _posZ);
    }
}
