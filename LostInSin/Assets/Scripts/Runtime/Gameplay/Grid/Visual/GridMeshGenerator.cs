using System.Collections.Generic;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Grid.Visual
{
	public class GridMeshGenerator
	{
		public Mesh CreateGridMesh(GridCellData[,] gridCells)
		{
			List<Vector3> vertices = new();
			List<Vector2> uvs = new(); // List for UVs
			List<int> triangles = new();

			var width = gridCells.GetLength(0);
			var height = gridCells.GetLength(1);

			for (var x = 0; x < width; x++)
				for (var y = 0; y < height; y++)
					if (!gridCells[x, y].IsInvalid)
						AddCellToMesh(gridCells[x, y], vertices, uvs, triangles, x, y, width, height);

			Mesh mesh = new();
			mesh.vertices = vertices.ToArray();
			mesh.triangles = triangles.ToArray();
			mesh.uv = uvs.ToArray(); // Assign UVs to the mesh
			mesh.RecalculateNormals();

			return mesh;
		}

		private void AddCellToMesh(GridCellData cellData,
			List<Vector3> vertices,
			List<Vector2> uvs,
			List<int> triangles,
			int x,
			int y,
			int gridWidth,
			int gridHeight)
		{
			var vertexIndex = vertices.Count;

			vertices.Add(cellData.TopLeft.ToVector3());
			vertices.Add(cellData.TopRight.ToVector3());
			vertices.Add(cellData.BottomLeft.ToVector3());
			vertices.Add(cellData.BottomRight.ToVector3());

			// Calculate UVs based on the cell's position within the grid
			var uvWidth = 1f / gridWidth;
			var uvHeight = 1f / gridHeight;
			Vector2 uvTopLeft = new(x * uvWidth, y * uvHeight);
			Vector2 uvTopRight = new((x + 1) * uvWidth, y * uvHeight);
			Vector2 uvBottomLeft = new(x * uvWidth, (y + 1) * uvHeight);
			Vector2 uvBottomRight = new((x + 1) * uvWidth, (y + 1) * uvHeight);

			// Assign UVs for each vertex
			uvs.Add(uvTopLeft);
			uvs.Add(uvTopRight);
			uvs.Add(uvBottomLeft);
			uvs.Add(uvBottomRight);

			// Add triangles (two triangles forming a quad)
			// top-left triangle
			triangles.Add(vertexIndex + 1); //top right
			triangles.Add(vertexIndex); //top left
			triangles.Add(vertexIndex + 2); // bottom left

			// bottom-right triangle
			triangles.Add(vertexIndex + 2); //bottom left 
			triangles.Add(vertexIndex + 3); //bottom right
			triangles.Add(vertexIndex + 1); //top right
		}
	}
}