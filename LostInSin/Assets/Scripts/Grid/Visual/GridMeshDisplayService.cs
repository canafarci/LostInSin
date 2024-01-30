using System;
using LostInSin.Grid.DataObjects;
using UnityEngine;
using Zenject;

namespace LostInSin.Grid.Visual
{
    public class GridMeshDisplayService
    {
        private readonly GridModel _gridModel;
        private readonly GridMeshGenerator _gridMeshGenerator;
        private readonly Data _data;
        private readonly int _gridSize = Shader.PropertyToID("_GridSize");

        private GridMeshDisplayService(GridModel model, GridMeshGenerator meshGenerator, Data data)
        {
            _gridModel = model;
            _gridMeshGenerator = meshGenerator;
            _data = data;
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

            Mesh gridMesh = _gridMeshGenerator.CreateGridMesh(_gridModel.GridCells);
            filter.mesh = gridMesh;

            Material mat = new(_data.GridShader);
            mat.SetFloat(_gridSize, _gridModel.GridColumnCount);

            renderer.material = mat;

            gridObject.transform.position = Vector3.up / 30f;
        }

        [Serializable]
        public class Data
        {
            [SerializeField] private GridVisualDataSO _gridVisualDataSO;
            public Shader GridShader => _gridVisualDataSO.GridShader;
        }
    }
}