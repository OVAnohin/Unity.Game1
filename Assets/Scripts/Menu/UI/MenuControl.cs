using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{
  [SerializeField] private Button _startGame = default;
  [SerializeField] private Button _exitGame = default;
  [SerializeField] private Button _showAuthors = default;
  [SerializeField] private string _sceneName = default;
  [SerializeField] private GameObject _authorsPanel = default;

  private void OnEnable()
  {
    _startGame.onClick.AddListener(LoadScene);
    _showAuthors.onClick.AddListener(ShowAuthors);
    _exitGame.onClick.AddListener(ExitGame);
  }

  private void OnDisable()
  {
    _startGame.onClick.RemoveListener(LoadScene);
    _showAuthors.onClick.AddListener(ShowAuthors);
    _exitGame.onClick.RemoveListener(ExitGame);
  }

  private void ShowAuthors()
  {
    _authorsPanel.SetActive(true);
  }


  private void LoadScene()
  {
    SceneManager.LoadScene(_sceneName);
  }

  private void ExitGame()
  {
    Application.Quit();
  }
}
