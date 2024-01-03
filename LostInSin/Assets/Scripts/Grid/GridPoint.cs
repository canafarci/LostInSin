using UnityEngine;

namespace LostInSin.Grid
{
    public struct GridPoint
    {
        public float PosX => _posX;
        public float PosY => _posY;
        public float PosZ => _posZ;
        public bool IsVoid => _isVoid;
        private readonly float _posX;
        private readonly float _posY;
        private readonly float _posZ;
        private readonly bool _isVoid;

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
