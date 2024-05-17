using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => _instance;
    public event Action Won;
    public event Action Lost;
    public event Action Moved;

    [SerializeField] private SaveData _saveData;

    private static GameManager _instance;
    private ButtonHolder[] _buttonHolders;
    private int _piecesMoving;


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
            LoadSavedData();
        }
        else
            Destroy(this);
    }

    private void Start() => FindButtonHolders();

    private void FindButtonHolders() => _buttonHolders = FindObjectsOfType<ButtonHolder>();

    public void CheckForVictoryCondition()
    {
        var victory = true;
        foreach (var buttonHolder in _buttonHolders)
        {
            if(buttonHolder.HasCorrectButton)
                continue;
            victory = false;
        }

        if (victory && _piecesMoving <= 0)
        {
            Won?.Invoke();
            AudioManager.Instance.PlayVictorySFX();
            _saveData.NextUnBeatenLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SaveData();
        }
    }

    public void PieceMoving() => _piecesMoving++;

    public void PieceStopped()
    {
        _piecesMoving--;
        if (_piecesMoving == 0)
        {
            CheckForVictoryCondition();
        }
        Moved?.Invoke();
    }

    public void LevelFailed()
    {
        Lost?.Invoke();
        AudioManager.Instance.PlayLoseSFX();
    }

    public void NewLevelLoaded() => FindButtonHolders();

    private void LoadSavedData()
    {
        var levelIndex = PlayerPrefs.GetInt("levelIndex", 0);
        if (levelIndex == 0) return;

        _saveData.NextUnBeatenLevelIndex = levelIndex;
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("levelIndex", _saveData.NextUnBeatenLevelIndex);
        PlayerPrefs.Save();
    }

    public int GetNextLevelIndex() => _saveData.NextUnBeatenLevelIndex;
}