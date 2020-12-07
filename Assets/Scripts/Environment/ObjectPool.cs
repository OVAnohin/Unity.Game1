using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private int _capacity;
    [SerializeField] private Transform _checkPoint;

    protected GameObject Container => _container;

    private List<GameObject> _pool = new List<GameObject>();

    protected void Initialize(GameObject prefab)
    {
        SetCheckPoint();

        for (int i = 0; i < _capacity; i++)
        {
            GameObject spawned = Instantiate(prefab, _container.transform);
            spawned.SetActive(false);
            _pool.Add(spawned);
        }
    }

    private void SetCheckPoint()
    {
        float angle = Camera.main.fieldOfView;
        float radiantAngle = (90 - angle) * (Mathf.PI / 180);
        float tangens = Mathf.Tan(radiantAngle);

        float y = Mathf.Abs(Camera.main.transform.position.z) * tangens;
        float x = -(((Screen.currentResolution.width * y) / Screen.currentResolution.height) + 1);

        _checkPoint.position = new Vector3(x, 0, 0);
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
                if (_checkPoint.position.x > item.transform.position.x)
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
