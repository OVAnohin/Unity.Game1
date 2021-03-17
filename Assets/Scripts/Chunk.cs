using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class Chunk : MonoBehaviour
{
    [SerializeField] private ConfigurationData _configuration;
    [SerializeField] private List<GridElement> _items;
    [SerializeField] private Transform _begin;
    [SerializeField] private Transform _end;

    private int _chunkLength;
    private int _chunkWidth;
    private List<GridObject> _pool = new List<GridObject>();

    private void OnEnable()
    {
        _chunkLength = _configuration.ChunkLength;
        _chunkWidth = _configuration.ChunkWidth;

        foreach (var item in _items)
        {
            var spawned = Instantiate(item.Prefab, transform);
            spawned.transform.position = transform.position;
            spawned.SetActive(false);
            spawned.GetComponent<GridObject>().Init();
            item.Prefab = spawned;
        }

        GenerateChunk();
    }

    private void GenerateChunk()
    {
        foreach (var item in _items)
        {
            GridObject prefab = item.Prefab.GetComponent<GridObject>();

            for (int x = -_chunkLength; x < _chunkLength; x++)
                for (int z = -_chunkWidth; z < _chunkWidth; z++)
                    CreatePrefab(prefab);
        }
    }

    private void CreatePrefab(GridObject prefab)
    {
        var spawned = Instantiate(prefab, transform);
        spawned.Init();
        spawned.gameObject.SetActive(false);
        _pool.Add(spawned);
    }

    public Vector3 ResetChunk(Vector3 center)
    {
        transform.position = center;
        _begin.position = new Vector3(center.x - _chunkLength, transform.position.y, transform.position.z);
        _end.position = new Vector3(center.x + _chunkLength, transform.position.y, transform.position.z);

        StartCoroutine(ReSetChunk());

        return _end.position - _begin.localPosition;
    }

    private IEnumerator ReSetChunk()
    {
        foreach (var item in _pool)
            item.gameObject.SetActive(false);

        for (int x = -_chunkLength; x < _chunkLength; x++)
        {
            for (int z = -_chunkWidth; z < _chunkWidth; z++)
            {
                foreach (var item in _items)
                {
                    GridObject prefab = item.Prefab.GetComponent<GridObject>();
                    string nickName = prefab.NickName;
                    UpdateElementInPool(nickName, transform.position, x, z);
                }
            }
        }

        yield return null;
    }

    private void UpdateElementInPool(string nickName, Vector3 center, int x, int z)
    {
        var item = _pool.Find(p => p.gameObject.activeSelf == false && p.NickName == nickName);
        if (item.GetComponent<GridObject>().Chance > Random.Range(0, 100))
        {
            item.transform.position = new Vector3(center.x + x, (int)item.Layer, center.z + z);
            item.gameObject.SetActive(true);
        }
    }
}

[System.Serializable]
public class GridElement
{
    public GameObject Prefab;
}