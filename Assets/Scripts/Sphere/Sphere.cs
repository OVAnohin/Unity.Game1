using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Sphere : MonoBehaviour
{
  private int _money;

  public event UnityAction<int> ScoreChanged;
  public event UnityAction Died;

  private void Start()
  {
    ScoreChanged?.Invoke(_money);
  }

  public void TakeDamage()
  {
    Die();
  }

  public void TakeBonus()
  {
    _money++;
    ScoreChanged?.Invoke(_money);
  }

  public void Die()
  {
    Died?.Invoke();
    Destroy(gameObject);
  }
}
