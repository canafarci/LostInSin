using UnityEngine;
using Sirenix.OdinInspector;

namespace LostInSin.Grid
{
    [CreateAssetMenu(fileName = "GridGenerationData", menuName = "LostInSin/GridGenerationData", order = 0)]
    public class GridGenerationSO : SerializedScriptableObject
    {
        public int GridXSize;
        public int GridYSize;
        public int GridWidth;
        public int GridHeight;
    }
}
