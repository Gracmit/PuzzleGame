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
        _winScreen = GetComponentInChildren<Image>().gameObject;
        _winScreen.SetActive(false);
    }

    public void Show()
    {
        _winScreen.SetActive(true);
    }

    public void ReturnToHomeScreen()
    {
        //Todo
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        //Todo
    }
}
