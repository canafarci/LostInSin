using Sirenix.OdinInspector;
using UnityEngine;

namespace LostInSin.Runtime.Grid.DataObjects
{
	[CreateAssetMenu(fileName = "GridGenerationData", menuName = "LostInSin/GridGenerationData", order = 0)]
	public class GridGenerationSO : SerializedScriptableObject
	{
		public int GridXSize;
		public int GridYSize;
		public int GridRowCount;
		public int GridColumnCount;
	}
}