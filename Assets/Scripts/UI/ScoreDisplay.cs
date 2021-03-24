using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _scoreDisplay;

    private void OnEnable()
    {
        _player.ScoreChanged += DisplayScore;
    }

    private void OnDisable()
    {
        _player.ScoreChanged -= DisplayScore;
    }

    private void DisplayScore(int score)
    {
        _scoreDisplay.text = score.ToString();
    }
}
