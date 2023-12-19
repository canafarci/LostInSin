using UnityEngine;

namespace LostInSin.Grid
{
    public struct GridCell
    {
        public GridPoint TopLeft;
        public GridPoint TopRight;
        public GridPoint BottomLeft;
        public GridPoint BottomRight;
        public GridPoint Center;
        public bool IsInvalid;

        public GridCell(GridPoint topLeft,
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
        private static GridPoint CalculateCenter(GridPoint topLeft, GridPoint topRight, GridPoint bottomLeft, GridPoint bottomRight)
        {
            return new GridPoint(
                (topLeft.PosX + topRight.PosX + bottomLeft.PosX + bottomRight.PosX) / 4,
                (topLeft.PosY + topRight.PosY + bottomLeft.PosY + bottomRight.PosY) / 4,
                (topLeft.PosZ + topRight.PosZ + bottomLeft.PosZ + bottomRight.PosZ) / 4,
                topLeft.IsVoid && topRight.IsVoid && bottomLeft.IsVoid && bottomRight.IsVoid
            );
        }

        public void SetAllPointsToMinimumY()
        {
            float minY = Mathf.Min(TopLeft.PosY, TopRight.PosY, BottomLeft.PosY, BottomRight.PosY);
            TopLeft = new GridPoint(TopLeft.PosX, minY, TopLeft.PosZ, TopLeft.IsVoid);
            TopRight = new GridPoint(TopRight.PosX, minY, TopRight.PosZ, TopRight.IsVoid);
            BottomLeft = new GridPoint(BottomLeft.PosX, minY, BottomLeft.PosZ, BottomLeft.IsVoid);
            BottomRight = new GridPoint(BottomRight.PosX, minY, BottomRight.PosZ, BottomRight.IsVoid);
            Center = new GridPoint(Center.PosX, minY, Center.PosZ, Center.IsVoid);
        }
    }
}
