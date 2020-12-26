using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ObjectPool : MonoBehaviour
{
    private List<GridObject> _pool = new List<GridObject>();

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
        GridObject[] variants = _pool.Where(p => p.gameObject.activeSelf == false && p.Layer == layer).ToArray();
        result = null;

        if (variants.Count() > 0)
        {
            variants.Shuffle();

            result = variants.First();
            if (result.Chance == 100 || result.Chance > Random.Range(0, 100))
                return result != null;
        }

        return false;
    }
}
