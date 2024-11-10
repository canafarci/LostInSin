using UnityEngine;

namespace LostInSin.Runtime.Grid.DataObjects
{
	[CreateAssetMenu(fileName = "GridVisualData", menuName = "LostInSin/GridVisualData", order = 0)]
	public class GridVisualDataSO : ScriptableObject
	{
		public Shader GridShader;
	}
}