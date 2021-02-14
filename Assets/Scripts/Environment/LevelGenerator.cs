using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : ObjectPool
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _viewRadius;
    [SerializeField] private float _cellSize;

    private List<MatrixElement> _collisionsMatrix = new List<MatrixElement>();
    private int _cellCountOnAxis;
    private int _oneHundredPercent;

    private void Awake()
    {
        Initialize();
        _cellCountOnAxis = (int)(_viewRadius / _cellSize);
        _oneHundredPercent = (int)Mathf.Pow(_cellCountOnAxis * 2, 2);
    }

    private void Update()
    {
        FillRadius(_player.position);
        UnFillElements(_player.position);
    }

    private void FillRadius(Vector3 center)
    {
        var fillAreaCenter = WorldToGridPosition(center);
        for (int x = _cellCountOnAxis; x > -_cellCountOnAxis; x--)
        {
            for (int z = _cellCountOnAxis; z > -_cellCountOnAxis; z--)
            {
                TryCreateOnLayer(GridLayer.Ground, fillAreaCenter + new Vector3Int(x, 0, z));
                TryCreateOnLayer(GridLayer.OnGround, fillAreaCenter + new Vector3Int(x, 0, z));
            }
        }
    }

    private void UnFillElements(Vector3 center)
    {
        var areaCenter = WorldToGridPosition(center);

        MatrixElement[] distantElements = new MatrixElement[_collisionsMatrix.ToArray().Length];
        Array.Copy(_collisionsMatrix.ToArray(), distantElements, _collisionsMatrix.ToArray().Length);

        foreach (var element in distantElements)
        {
            var magnitudeZ = new Vector3Int(0, 0, element.Position.z - areaCenter.z);
            var magnitudeX = new Vector3Int(element.Position.x - areaCenter.x, 0, 0);
            if (magnitudeX.magnitude > _cellCountOnAxis || magnitudeZ.magnitude > _cellCountOnAxis)
            {
                element.Item.gameObject.SetActive(false);
                _collisionsMatrix.Remove(element);
            }
        }
    }

    private void TryCreateOnLayer(GridLayer layer, Vector3Int gridPosition)
    {
        gridPosition.y = (int)layer;
        if (_collisionsMatrix.Find(p => p.Position == gridPosition) != null)
            return;

        GridObject element = GetObject(layer);

        if (element.Chance == 100)
        {
            ActiveElementInHierarchy(gridPosition, element);
        }
        else 
        {
            var chance = (element.Chance * _oneHundredPercent) / 100;

            if (_collisionsMatrix.FindAll(e => e.Item.Chance == element.Chance).Count < chance && element.Chance > Random.Range(0, 100))
                ActiveElementInHierarchy(gridPosition, element);
        }
    }

    private void ActiveElementInHierarchy(Vector3Int gridPosition, GridObject element)
    {
        Vector3 position = GridToWorldPosition(gridPosition);
        element.gameObject.SetActive(true);
        element.transform.position = position;
        MatrixElement matrixElement = new MatrixElement(gridPosition, element);
        _collisionsMatrix.Add(matrixElement);
    }

    private Vector3Int WorldToGridPosition(Vector3 worldPosition)
    {
        return new Vector3Int((int)(worldPosition.x / _cellSize), (int)(worldPosition.y / _cellSize), (int)(worldPosition.z / _cellSize));
    }

    private Vector3 GridToWorldPosition(Vector3Int gridPosition)
    {
        return new Vector3(gridPosition.x * _cellSize, gridPosition.y * _cellSize, gridPosition.z * _cellSize);
    }

    private class MatrixElement
    {
        public Vector3Int Position { get; private set; }
        public GridObject Item { get; private set; }

        public MatrixElement(Vector3Int position, GridObject item)
        {
            Position = position;
            Item = item;
        }
    }
}