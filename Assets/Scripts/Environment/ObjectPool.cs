using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool : MonoBehaviour
{
    [SerializeField] private List<PoolItem> _items;
    
    private List<GridObject> _pooledItems;

    protected void Initialize()
    {
        _pooledItems = new List<GridObject>();

        foreach (PoolItem item in _items)
        {
            for (int i = 0; i < item.Amount; i++)
            {
                GridObject gredObject = item.Prefab.GetComponent<GridObject>();
                SpawnGridObject(gredObject);
            }
        }
    }

    protected GridObject GetObject(GridLayer layer)
    {
        GridObject result;

        GridObject[] variants = _pooledItems.FindAll(p => p.gameObject.activeSelf == false && p.Layer == layer).ToArray();

        if (variants.Length > 0)
        {
            variants.Shuffle();
            result = variants[0];
        }
        else
        {
            GridObject gredObject = _items.Find(p => p.Prefab.GetComponent<GridObject>().Layer == layer).Prefab.GetComponent<GridObject>();
            SpawnGridObject(gredObject);
            result = _pooledItems.Find(p => p.gameObject.activeSelf == false && p.Layer == layer);
        }

        return result;
    }

    private void SpawnGridObject(GridObject gredObject)
    {
        GridObject spawned = Instantiate(gredObject, Vector3.zero, Quaternion.identity, transform);
        spawned.gameObject.SetActive(false);
        _pooledItems.Add(spawned);
    }
}

[System.Serializable]
public class PoolItem
{
    public GameObject Prefab;
    public int Amount;
}