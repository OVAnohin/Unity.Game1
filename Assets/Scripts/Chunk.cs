using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Chunk : MonoBehaviour
{
    [SerializeField] private List<GridElement> _items;
    [SerializeField] private int _chunkLength;
    [SerializeField] private int _chunkWidth;
    [SerializeField] private int _tick;

    private List<GridObject> _pool = new List<GridObject>();
    private int _elapsed;

    public int ChunkLength => _chunkLength;

    private void OnEnable()
    {
        GenerateChunk();
    }

    private void GenerateChunk()
    {
        for (int x = -_chunkLength; x < _chunkLength; x++)
            for (int z = -_chunkWidth; z < _chunkWidth; z++)
                foreach (var item in _items)
                    CreateObject(item.Prefab);
    }

    private void CreateObject(GridObject prefab)
    {
        var spawned = Instantiate(prefab, transform);
        spawned.gameObject.SetActive(false);
        _pool.Add(spawned);
    }

    public Vector3 ResetChunk(Vector3 center)
    {
        transform.position = center;

        StartCoroutine(Reset());

        return new Vector3(center.x + _chunkLength * 2, center.y, center.z);
    }

    private IEnumerator Reset()
    {
        _elapsed = 0;

        foreach (var item in _pool)
            item.gameObject.SetActive(false);

        for (int x = -_chunkLength; x < _chunkLength; x++)
        {
            for (int z = -_chunkWidth; z < _chunkWidth; z++)
            {
                foreach (var item in _items)
                    UpdateElementInPool(item.Prefab.NickName, transform.position, x, z);

                if (_elapsed == _tick)
                {
                    _elapsed = 0;
                    yield return null;
                }

                _elapsed++;
            }
        }
    }

    private void UpdateElementInPool(string nickName, Vector3 center, int x, int z)
    {
        var item = _pool.Find(p => p.gameObject.activeSelf == false && p.NickName == nickName);
        if (item.Chance > Random.Range(0, 100))
        {
            item.transform.position = new Vector3(center.x + x, (int)item.Layer, center.z + z);
            item.gameObject.SetActive(true);
        }
    }
}

[System.Serializable]
public class GridElement
{
    public GridObject Prefab;
}