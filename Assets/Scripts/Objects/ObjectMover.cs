using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
  [SerializeField] private float _moveSpeed = 0;

  private void Update()
  {
    transform.Translate(Vector3.left * _moveSpeed * Time.deltaTime);
  }
}
