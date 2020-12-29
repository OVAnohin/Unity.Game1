using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _distance;
    [SerializeField] private float _height;
    [SerializeField] private float _delay;

    private Vector3 _constantYZ;

    private void Start()
    {
        _constantYZ = _target.position + _target.up * _height - _target.forward * _distance;
        transform.position = _constantYZ;
    }

    private void LateUpdate()
    {
        Vector3 targetPos = new Vector3(_target.position.x, _constantYZ.y, _constantYZ.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * _delay);
    }
}
