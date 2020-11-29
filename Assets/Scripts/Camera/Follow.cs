using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    
    private Vector3 _offset;

    private void Start()
    {
        _offset = transform.position;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(_target.position.x, _offset.y, _offset.z);
    }
}
