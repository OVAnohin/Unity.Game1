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

        List<Transform> childrens = GetComponentsInChildren<Transform>().ToList();
        var gridObjects = childrens.Where(wv => wv.GetComponent<GridObject>() != false).ToList();

        if (gridObjects.Count > 0)
        {
            foreach (var item in gridObjects)
            {
                if ((areaCenter - item.position).magnitude > cellCountOnAxis || (areaCenter - item.position).magnitude > cellCountOnAxis)
                {
                    var position = WorldToGridPosition(item.position);
                    _collisionsMatrix.Remove(position);
                    item.gameObject.SetActive(false);
                }
            }
        }
    }

    private void TryCreateOnLayer(GridLayer layer, Vector3Int gridPosition)
    {
        gridPosition.y = (int)layer;

        if (_collisionsMatrix.Contains(gridPosition))
            return;
        else
            _collisionsMatrix.Add(gridPosition);

        if (TryGetObject(out GridObject element, layer))
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
