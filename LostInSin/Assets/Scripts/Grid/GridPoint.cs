using UnityEngine;

namespace LostInSin.Grid
{
    public struct GridPoint
    {
        public float PosX { get; }
        public float PosY { get; }
        public float PosZ { get; }
        public bool IsVoid { get; }

        public GridPoint(float posX, float posY, float posZ, bool isVoid = true)
        {
            PosX = posX;
            PosY = posY;
            PosZ = posZ;
            IsVoid = isVoid;
        }

        public readonly Vector3 ToVector3()
        {
            return new Vector3(PosX, PosY, PosZ);
        }
    }
}