using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
  [SerializeField] private Transform _target;
  [SerializeField] private Transform _camera;
  [SerializeField] private float _deltaX;

  private void LateUpdate()
  {
    _camera.position = new Vector3(_target.position.x + _deltaX, 0, -10);
  }
}
