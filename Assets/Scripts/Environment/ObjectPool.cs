using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private int _capacity;

    protected GameObject Container => _container;

    private List<GameObject> _pool = new List<GameObject>();
    private Camera _camera;


    protected void Initialize(GameObject prefab)
    {
        _camera = Camera.main;

        for (int i = 0; i < _capacity; i++)
        {
            GameObject spawned = Instantiate(prefab, _container.transform);
            spawned.SetActive(false);
            _pool.Add(spawned);
        }
    }

    protected void Initialize(GameObject[] prefabs)
    {
        for (int i = 0; i < _capacity; i++)
        {
            int index = Random.Range(0, prefabs.Length);
            GameObject spawned = Instantiate(prefabs[index], _container.transform);
            spawned.SetActive(false);

            _pool.Add(spawned);
        }
    }

    protected void DisableObjectAbroadScreen()
    {
        foreach (var item in _pool)
        {
            if (item.activeSelf == true)
            {
                Vector3 point = _camera.WorldToViewportPoint(item.transform.position);
                if (point.x < -0.5)
                {
                    item.SetActive(false);
                    item.transform.parent = Container.transform;
                }
            }
        }
    }

    protected bool TryGetObject(out GameObject result)
    {
        result = _pool.FirstOrDefault(p => p.activeSelf == false);

        return result != null;
    }
}
