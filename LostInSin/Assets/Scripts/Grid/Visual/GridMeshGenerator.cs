using System.Collections.Generic;
using UnityEngine;

namespace LostInSin.Grid
{
    public class GridMeshGenerator
    {
        public Mesh CreateGridMesh(GridCell[,] gridCells)
        {
            List<Vector3> vertices = new List<Vector3>();
            List<int> triangles = new List<int>();

            for (int x = 0; x < gridCells.GetLength(0); x++)
            {
                for (int y = 0; y < gridCells.GetLength(1); y++)
                {
                    if (!gridCells[x, y].IsInvalid)
                    {
                        AddCellToMesh(gridCells[x, y], vertices, triangles);
                    }
                }
            }

            Mesh mesh = new Mesh();
            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            mesh.RecalculateNormals();

            return mesh;
        }

        private void AddCellToMesh(GridCell cell, List<Vector3> vertices, List<int> triangles)
        {
            int vertexIndex = vertices.Count;

            vertices.Add(cell.TopLeft.ToVector3());
            vertices.Add(cell.TopRight.ToVector3());
            vertices.Add(cell.BottomLeft.ToVector3());
            vertices.Add(cell.BottomRight.ToVector3());

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
