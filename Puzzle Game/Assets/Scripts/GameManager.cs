using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private WinScreenUI _winScreen;

    public static GameManager Instance => _instance;

    private ButtonHolder[] _buttonHolders;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(this);
    }

    private void Start()
    {
        FindButtonHolders();
        _winScreen = FindObjectOfType<WinScreenUI>();
    }

    public void FindButtonHolders()
    {
        _buttonHolders = FindObjectsOfType<ButtonHolder>();
    }

    public void CheckForVictoryCondition()
    {
        bool victory = true;
        foreach (var buttonHolder in _buttonHolders)
        {
            if(buttonHolder.HasCorrectButton)
                continue;
            victory = false;
        }

        if (victory)
        {
            _winScreen.Show();
        }
    }
}
