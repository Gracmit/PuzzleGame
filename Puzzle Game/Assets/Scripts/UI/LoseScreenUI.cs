using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseScreenUI : MonoBehaviour
{
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _restartButton;
    private GameObject _loseScreen;
    private void Awake()
    {
        _loseScreen = GetComponentInChildren<Image>().gameObject;
        _loseScreen.SetActive(false);
    }

    private void Start()
    {
        GameManager.Instance.Lost += HandleLostState;
    }

    private void OnDestroy()
    {
        GameManager.Instance.Lost -= HandleLostState;
    }

    private void HandleLostState()
    {
        _loseScreen.SetActive(true);
    }
    
    public void ReturnToHomeScreen()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}