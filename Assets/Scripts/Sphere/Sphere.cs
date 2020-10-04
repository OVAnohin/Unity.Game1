using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Sphere : MonoBehaviour
{
    public event UnityAction<int> ScoreChanged;

    private int _score = 0;

    private void Start()
    {
        ScoreChanged?.Invoke(_score);
    }

    public void TakeCoin()
    {
        _score += 1;
        ScoreChanged?.Invoke(_score);
    }
}
