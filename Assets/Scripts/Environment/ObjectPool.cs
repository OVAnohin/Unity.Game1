using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool : MonoBehaviour
{
    private List<GridObject> _pool = new List<GridObject>();

    protected List<GridObject> Pool => _pool;

    protected void Initialize(GridObject prefab)
    {
        int capacity = prefab.Capacity;
        for (int i = 0; i < capacity; i++)
        {
            GridObject spawned = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform);
            spawned.gameObject.SetActive(false);
            _pool.Add(spawned);
        }
    }

    protected bool TryGetObject(out GridObject result, GridLayer layer)
    {
        GridObject[] variants = _pool.FindAll(p => p.gameObject.activeSelf == false && p.Layer == layer).ToArray();
        result = null;

        if (variants.Length > 0)
        {
            variants.Shuffle();

            result = variants[0];
            if (result.Chance == 100 || result.Chance > Random.Range(0, 100))
                return result != null;
        }

        return false;
    }
}