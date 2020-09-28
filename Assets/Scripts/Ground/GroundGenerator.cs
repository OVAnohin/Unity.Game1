using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
  [SerializeField] private GameObject _ground;
  [SerializeField] private Transform _groundPool;
  [SerializeField] private Sphere _player;
  [SerializeField] private int _x;
  [SerializeField] private int _z;

  private int _startX;
  private int _startZ;
  private int _nextStep = 1;
  private Queue<GameObject> _grounds = new Queue<GameObject>();
  private int _deltaX;
  private int _lastX;

  private void Start()
  {
    _startX = (_x / 3) * -1;
    _startZ = (_z / 3) * -1;
    _x += _startX;
    _z += _startZ;
    _deltaX = _x + (-_startX);
    _lastX = _x;

    for (int x = _startX; x < _x; x++)
      for (int z = _startZ; z < _z; z++)
      {
        GameObject spawned = Instantiate(_ground, _groundPool);
        spawned.transform.position = new Vector3(x, _groundPool.position.y, z);
        _grounds.Enqueue(spawned);
      }
  }

  private void Update()
  {
    if (_player.transform.position.x >= _nextStep)
    {
      _nextStep += 1;

      while ((_grounds.Peek().transform.position.x * -1) + _lastX == _deltaX)
      {
        GameObject element = _grounds.Dequeue();
        element.transform.position = new Vector3(_lastX, element.transform.position.y, element.transform.position.z);
        _grounds.Enqueue(element);
      }

      _lastX += 1;
    }
  }

}
