using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
  [SerializeField] private GameObject _ground;
  [SerializeField] private Transform _spawnPoint;

  private void Start()
  {
    for (int x = -10; x < 20; x++)
    {
      for (int z = -10; z < 10; z++)
      {
        var spawned = Instantiate(_ground, _spawnPoint);
        spawned.transform.position = new Vector3(x, _spawnPoint.position.y, z);
      }
    }
  }

}
