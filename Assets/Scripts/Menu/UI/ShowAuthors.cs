using UnityEngine;
using DG.Tweening;
using System;

public class ShowAuthors : MonoBehaviour
{
  [SerializeField] private float _time = 0;

  private Vector2 _startPosition;
  private Vector2 _topPosition;

  private void Awake()
  {
    _startPosition = transform.position;
    _topPosition = new Vector2(transform.position.x, Screen.height * 0.95f + transform.position.y);
  }

  private void OnEnable()
  {
    transform.position = _startPosition;
    transform.DOMove(_topPosition, _time).SetEase(Ease.Linear);
  }
}
