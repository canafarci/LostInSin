using UnityEngine;

namespace LostInSin.Runtime.Grid
{
	public struct GridCellData
	{
		public GridPoint TopLeft;
		public GridPoint TopRight;
		public GridPoint BottomLeft;
		public GridPoint BottomRight;
		public GridPoint Center;
		public bool IsInvalid;

		public GridCellData(GridPoint topLeft,
			GridPoint topRight,
			GridPoint bottomLeft,
			GridPoint bottomRight,
			bool isInvalid = true)
		{
			TopLeft = topLeft;
			TopRight = topRight;
			BottomLeft = bottomLeft;
			BottomRight = bottomRight;
			IsInvalid = isInvalid;

			if (!isInvalid)
				Center = CalculateCenter(topLeft, topRight, bottomLeft, bottomRight);
			else
				Center = default;
		}

		private static GridPoint CalculateCenter(GridPoint topLeft, GridPoint topRight, GridPoint bottomLeft,
			GridPoint bottomRight)
		{
			return new GridPoint(
				(topLeft.posX + topRight.posX + bottomLeft.posX + bottomRight.posX) / 4,
				(topLeft.posY + topRight.posY + bottomLeft.posY + bottomRight.posY) / 4,
				(topLeft.posZ + topRight.posZ + bottomLeft.posZ + bottomRight.posZ) / 4,
				topLeft.isVoid || topRight.isVoid || bottomLeft.isVoid || bottomRight.isVoid
			);
		}

		public void SetAllPointsToMinimumY()
		{
			var minY = Mathf.Min(TopLeft.posY, TopRight.posY, BottomLeft.posY, BottomRight.posY);
			TopLeft = new GridPoint(TopLeft.posX, minY, TopLeft.posZ, TopLeft.isVoid);
			TopRight = new GridPoint(TopRight.posX, minY, TopRight.posZ, TopRight.isVoid);
			BottomLeft = new GridPoint(BottomLeft.posX, minY, BottomLeft.posZ, BottomLeft.isVoid);
			BottomRight = new GridPoint(BottomRight.posX, minY, BottomRight.posZ, BottomRight.isVoid);
			Center = new GridPoint(Center.posX, minY, Center.posZ, Center.isVoid);
		}
	}
}