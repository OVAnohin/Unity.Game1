using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{
  [SerializeField] private Button _startGame;
  [SerializeField] private Button _exitGame;
  [SerializeField] private Button _showAuthors;
  [SerializeField] private string _sceneName;
  [SerializeField] private GameObject _authorsPanel;

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
