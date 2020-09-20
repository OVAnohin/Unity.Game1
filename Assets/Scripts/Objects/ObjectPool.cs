using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
  [SerializeField] private GameObject _container;
  [SerializeField] private int _capacity;

  private List<SomeObject> _pool = new List<SomeObject>();

  protected void Initialize(SomeObject prefab)
  {
    for (int i = 0; i < _capacity; i++)
    {
      SomeObject spawned = Instantiate(prefab, _container.transform);
      spawned.gameObject.SetActive(false);

      _pool.Add(spawned);
    }
  }

  protected bool TryGetObject(out SomeObject result)
  {
    result = _pool.FirstOrDefault(p => p.gameObject.activeSelf == false);

    return result != null;
  }
}
