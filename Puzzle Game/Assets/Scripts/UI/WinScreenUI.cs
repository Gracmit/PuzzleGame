using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreenUI : MonoBehaviour
{
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _nextButton;
    private GameObject _winScreen;
    private void Awake()
    {
        _winScreen = null;
        _winScreen = GetComponentInChildren<Image>().gameObject;
        _winScreen.SetActive(false);
    }

    private void Start()
    {
        GameManager.Instance.Won += HandleWonState;
    }
    
    private void OnDestroy()
    {
        GameManager.Instance.Won -= HandleWonState;
    }

    private void HandleWonState()
    {
        _winScreen.SetActive(true);
    }
    
    public void ReturnToHomeScreen()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}