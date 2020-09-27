using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class SphereMover : MonoBehaviour
{
  [SerializeField] private float _speed;
  [SerializeField] private float _zAngle;
  [SerializeField] private float _jumpForce;

  private Rigidbody _rigidbody;

  private void Awake()
  {
    _rigidbody = GetComponent<Rigidbody>();
  }

  private void Update()
  {
    if (Input.GetKey(KeyCode.Space) && _rigidbody.velocity == Vector3.zero)
    {
      _rigidbody.velocity = Vector3.up * _jumpForce;
    }

    transform.position = transform.position + Vector3.right * _speed * Time.deltaTime;
    transform.Rotate(0, 0, _zAngle);

  }
}
