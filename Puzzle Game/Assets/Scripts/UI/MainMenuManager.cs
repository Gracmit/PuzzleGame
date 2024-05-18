using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    private OptionsMenu _options;
    private ResetDataMenu _resetDataMenu;
    private CreditsMenu _creditsMenu;
    private static MainMenuManager _instance;
    private MainMenu _mainMenu;

    public static MainMenuManager Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        _options = GetComponentInChildren<OptionsMenu>();
        _resetDataMenu = GetComponentInChildren<ResetDataMenu>();
        _creditsMenu = GetComponentInChildren<CreditsMenu>();
        _mainMenu = GetComponentInChildren<MainMenu>();
        GameManager.Instance.SetVolume();
    }

    public void OpenOptionsMenu() => _options.Open();

    public void OpenResetSavedDataMenu() => _resetDataMenu.Open();

    public void OpenCreditsMenu() => _creditsMenu.Open();

    public void OpenMainMenu() => _mainMenu.Open();
}
