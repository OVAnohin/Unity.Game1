using UnityEngine;
using DG.Tweening;
using System;

public class ShowAuthors : MonoBehaviour
{
    [SerializeField] private float _time = 0;
    [SerializeField] private float _offset;

    private Vector2 _startPosition;
    private Vector2 _topPosition;

    private void Awake()
    {
        _startPosition = transform.position;
        _topPosition = new Vector2(transform.position.x, Screen.height * _offset + transform.position.y);
    }

    private void OnEnable()
    {
        transform.position = _startPosition;
        transform.DOMove(_topPosition, _time).SetEase(Ease.Linear);
    }
}
