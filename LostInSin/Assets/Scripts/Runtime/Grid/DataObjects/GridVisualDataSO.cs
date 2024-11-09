using UnityEngine;

namespace LostInSin.Runtime.Grid.DataObjects
{
	[CreateAssetMenu(fileName = "GridVisualData", menuName = "LostInSin/GridVisualData", order = 0)]
	public class GridVisualDataSo : ScriptableObject
	{
		public Shader GridShader;
	}
}