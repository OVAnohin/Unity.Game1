using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _vectorOffcet;
    [SerializeField] private float _speed;

    private void Start()
    {
        transform.position = _target.position + _vectorOffcet;
    }

    private void Update()
    {
        Vector3 targetPosition = new Vector3(_target.position.x + _vectorOffcet.x, _vectorOffcet.y, _vectorOffcet.z);

        if (targetPosition.x != transform.position.x)
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
    }
}
