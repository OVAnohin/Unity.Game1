using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
  [SerializeField] private Sphere _sphere;
  [SerializeField] private TMP_Text _scoreDisplay;

  private void OnEnable()
  {
    _sphere.ScoreChanged += ChangeScoreOnDisplay;
  }

  private void OnDisable()
  {
    _sphere.ScoreChanged -= ChangeScoreOnDisplay;
  }

  private void ChangeScoreOnDisplay(int health)
  {
    _scoreDisplay.text = health.ToString();
  }
}
