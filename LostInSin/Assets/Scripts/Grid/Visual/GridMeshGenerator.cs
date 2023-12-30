using System.Collections.Generic;
using UnityEngine;

namespace LostInSin.Grid
{
    public class GridMeshGenerator
    {
        public Mesh CreateGridMesh(GridCell[,] gridCells)
        {
            List<Vector3> vertices = new List<Vector3>();
            List<Vector2> uvs = new List<Vector2>(); // List for UVs
            List<int> triangles = new List<int>();

            int width = gridCells.GetLength(0);
            int height = gridCells.GetLength(1);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (!gridCells[x, y].IsInvalid)
                    {
                        AddCellToMesh(gridCells[x, y], vertices, uvs, triangles, x, y, width, height);
                    }
                }
            }

            Mesh mesh = new Mesh();
            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            mesh.uv = uvs.ToArray(); // Assign UVs to the mesh
            mesh.RecalculateNormals();

            return mesh;
        }

        private void AddCellToMesh(GridCell cell,
                                   List<Vector3> vertices,
                                   List<Vector2> uvs,
                                   List<int> triangles,
                                   int x,
                                   int y,
                                   int gridWidth,
                                   int gridHeight)
        {
            int vertexIndex = vertices.Count;

            vertices.Add(cell.TopLeft.ToVector3());
            vertices.Add(cell.TopRight.ToVector3());
            vertices.Add(cell.BottomLeft.ToVector3());
            vertices.Add(cell.BottomRight.ToVector3());

            // Calculate UVs based on the cell's position within the grid
            float uvWidth = 1f / gridWidth;
            float uvHeight = 1f / gridHeight;
            Vector2 uvTopLeft = new Vector2(x * uvWidth, y * uvHeight);
            Vector2 uvTopRight = new Vector2((x + 1) * uvWidth, y * uvHeight);
            Vector2 uvBottomLeft = new Vector2(x * uvWidth, (y + 1) * uvHeight);
            Vector2 uvBottomRight = new Vector2((x + 1) * uvWidth, (y + 1) * uvHeight);

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
