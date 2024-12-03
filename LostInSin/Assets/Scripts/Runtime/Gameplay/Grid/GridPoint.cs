using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Grid
{
	public struct GridPoint
	{
		public float posX { get; }
		public float posY { get; }
		public float posZ { get; }
		public bool isVoid { get; }

		public GridPoint(float posX, float posY, float posZ, bool isVoid = true)
		{
			this.posX = posX;
			this.posY = posY;
			this.posZ = posZ;
			this.isVoid = isVoid;
		}

		public readonly Vector3 ToVector3()
		{
			return new Vector3(posX, posY, posZ);
		}
	}
}