using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : ObjectPool
{
    [SerializeField] private GridObject[] _objectPrefabs;
    [SerializeField] private Transform _player;
    [SerializeField] private float _viewRadius;
    [SerializeField] private float _cellSize;

    private HashSet<Vector3Int> _collisionsMatrix = new HashSet<Vector3Int>();

    private void Awake()
    {
        foreach (var item in _objectPrefabs)
            Initialize(item);
    }

    private void Update()
    {
        FillRadius(_player.position, _viewRadius);
        UnFillElements(_player.position, _viewRadius);
    }

    private void FillRadius(Vector3 center, float viewRadius)
    {
        var cellCountOnAxis = (int)(viewRadius / _cellSize);
        var fillAreaCenter = WorldToGridPosition(center);

        for (int x = -cellCountOnAxis; x < cellCountOnAxis; x++)
        {
            for (int z = -cellCountOnAxis; z < cellCountOnAxis; z++)
            {
                TryCreateOnLayer(GridLayer.Ground, fillAreaCenter + new Vector3Int(x, 0, z));
                TryCreateOnLayer(GridLayer.OnGround, fillAreaCenter + new Vector3Int(x, 0, z));
            }
        }
    }

    private void UnFillElements(Vector3 center, float viewRadius)
    {
        var cellCountOnAxis = (int)(viewRadius / _cellSize);
        var areaCenter = WorldToGridPosition(center);

        var gridObjects = from go in Pool
                          let magnitudeZ = new Vector3Int(0, 0, (int)go.transform.position.z - (int)areaCenter.z)
                          let magnitudeX = new Vector3Int((int)go.transform.position.x - (int)areaCenter.x, 0, 0)
                          where (magnitudeX.magnitude > cellCountOnAxis || magnitudeZ.magnitude > cellCountOnAxis)
                          select go;

        foreach (var item in gridObjects)
        {
            var position = WorldToGridPosition(item.transform.position);
            _collisionsMatrix.Remove(position);
            item.gameObject.SetActive(false);
        }
    }

    private void TryCreateOnLayer(GridLayer layer, Vector3Int gridPosition)
    {
        gridPosition.y = (int)layer;

        if (_collisionsMatrix.Contains(gridPosition))
            return;
        else
            _collisionsMatrix.Add(gridPosition);

        GridObject element;
        if (TryGetObject(out element, layer))
        {
            var position = GridToWorldPosition(gridPosition);
            element.gameObject.SetActive(true);
            element.transform.position = position;
        }
    }

    private Vector3Int WorldToGridPosition(Vector3 worldPosition)
    {
        return new Vector3Int((int)(worldPosition.x / _cellSize), (int)(worldPosition.y / _cellSize), (int)(worldPosition.z / _cellSize));
    }

    private Vector3 GridToWorldPosition(Vector3Int gridPosition)
    {
        return new Vector3(gridPosition.x * _cellSize, gridPosition.y * _cellSize, gridPosition.z * _cellSize);
    }
}