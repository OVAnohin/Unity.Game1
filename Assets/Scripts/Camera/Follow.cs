using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _selfTransform;
    [SerializeField] private float _offsetY;
    [SerializeField] private float _offsetZ;

    private void Start()
    {
        _offsetY = _target.position.y + _offsetY;
    }

    private void LateUpdate()
    {
        _selfTransform.position = new Vector3(_target.position.x, _offsetY, _offsetZ);
    }
}
