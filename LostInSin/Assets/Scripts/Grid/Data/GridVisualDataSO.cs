using UnityEngine;

namespace LostInSin.Grid
{
    [CreateAssetMenu(fileName = "GridVisualData", menuName = "LostInSin/GridVisualData", order = 0)]
    public class GridVisualDataSO : ScriptableObject
    {
        public Shader GridShader;
    }
}
