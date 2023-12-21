using UnityEngine;
using Zenject;

namespace LostInSin.Grid
{
    public class GridMeshDisplayService : IInitializable //TODO call draw grid from elsewhere
    {
        private readonly GridModel _gridModel;
        private readonly GridMeshGenerator _gridMeshGenerator;
        private readonly Data _data;

        private GridMeshDisplayService(GridModel model, GridMeshGenerator meshGenerator, Data data)
        {
            _gridModel = model;
            _gridMeshGenerator = meshGenerator;
            _data = data;
        }

        public void Initialize()
        {
            DisplayGrid();
        }

        public void DisplayGrid()
        {
            GameObject gridObject = new GameObject("grid-visual", typeof(MeshRenderer), typeof(MeshFilter));
            MeshFilter filter = gridObject.GetComponent<MeshFilter>();
            MeshRenderer renderer = gridObject.GetComponent<MeshRenderer>();

            Mesh gridMesh = _gridMeshGenerator.CreateGridMesh(_gridModel.GridCells);
            filter.mesh = gridMesh;

            Material mat = new Material(_data.GridShader);
            renderer.material = mat;

            gridObject.transform.position = Vector3.up / 30f;
        }

        public class Data
        {
            [SerializeField] private GridVisualDataSO _gridVisualDataSO;
            public Shader GridShader { get { return _gridVisualDataSO.GridShader; } }
        }
    }
}
