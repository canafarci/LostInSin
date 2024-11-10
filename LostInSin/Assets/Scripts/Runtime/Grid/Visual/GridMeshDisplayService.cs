using LostInSin.Runtime.Grid.DataObjects;
using UnityEngine;

namespace LostInSin.Runtime.Grid.Visual
{
	public class GridMeshDisplayService
	{
		private readonly GridModel _gridModel;
		private readonly GridMeshGenerator _gridMeshGenerator;
		private readonly GridVisualDataSO _visualData;
		private readonly int _gridSize = Shader.PropertyToID("_GridSize");

		private GridMeshDisplayService(GridModel model, GridMeshGenerator meshGenerator, GridVisualDataSO visualData)
		{
			_gridModel = model;
			_gridMeshGenerator = meshGenerator;
			_visualData = visualData;
		}

		public void ShowGrid()
		{
			DisplayGrid();
		}

		private void DisplayGrid()
		{
			GameObject gridObject = new("grid-visual", typeof(MeshRenderer), typeof(MeshFilter));
			MeshFilter filter = gridObject.GetComponent<MeshFilter>();
			MeshRenderer renderer = gridObject.GetComponent<MeshRenderer>();

			Mesh gridMesh = _gridMeshGenerator.CreateGridMesh(_gridModel.gridCells);
			filter.mesh = gridMesh;

			Material mat = new(_visualData.GridShader);
			mat.SetFloat(_gridSize, _gridModel.gridColumnCount);

			renderer.material = mat;

			gridObject.transform.position = Vector3.up / 30f;
		}
	}
}