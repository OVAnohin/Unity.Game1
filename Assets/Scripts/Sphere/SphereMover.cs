using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]

public class SphereMover : MonoBehaviour
{
  [SerializeField] private float _jumpForce = 0;
  [SerializeField] private float _rotateForce = 0;


  private Rigidbody2D _rigidbody2D;

  private void Start()
  {
    transform.DORotate(new Vector3(0, 0, -360f), _rotateForce, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
    _rigidbody2D = GetComponent<Rigidbody2D>();
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space) && _rigidbody2D.velocity == Vector2.zero)
    {
      _rigidbody2D.velocity = Vector2.up * _jumpForce;
    }
  }

}
