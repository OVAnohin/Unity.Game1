using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private Sphere _sphere;
    [SerializeField] private TMP_Text _scoreDisplay;

    private void OnEnable()
    {
        _sphere.ScoreChanged += DisplayScore;
    }

    private void OnDisable()
    {
        _sphere.ScoreChanged -= DisplayScore;
    }

    private void DisplayScore(int score)
    {
        _scoreDisplay.text = score.ToString();
    }
}
