using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : ObjectPool
{
  [SerializeField] private SomeObject _prefab;
  [SerializeField] private Transform _spawnPoint;
  [SerializeField] private float _secondsBetweenSpawn;

  private float _elapsedTime = 0;
  private System.Random rand = new System.Random();

  private void Start()
  {
    Initialize(_prefab);
  }

  private void Update()
  {
    _elapsedTime += Time.deltaTime;

    if (_elapsedTime >= _secondsBetweenSpawn)
    {
      if (TryGetObject(out SomeObject element))
      {
        _elapsedTime = 0;
        SetSomeObject(element, _spawnPoint.position);
      }
    }
  }

  private void SetSomeObject(SomeObject someObject, Vector3 spawnPoint)
  {
    someObject.gameObject.SetActive(true);
    someObject.transform.position = spawnPoint;
  }
}
