using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => _instance;
    public event Action Won;
    public event Action Lost;
    public event Action Moved;
    public event Action<float> OptionsLoaded;
    public event Action DataReset;

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
            Destroy(gameObject);
    }

    private void Start() => FindButtonHolders();

    private void FindButtonHolders() => _buttonHolders = FindObjectsOfType<ButtonHolder>();

    public void CheckForVictoryCondition()
    {
        var victory = true;
        foreach (var buttonHolder in _buttonHolders)
        {
            if (buttonHolder.HasCorrectButton)
                continue;
            victory = false;
        }

        if (victory && _piecesMoving <= 0)
        {
            Won?.Invoke();
            AudioManager.Instance.PlayVictorySFX();
            if (SceneManager.GetActiveScene().buildIndex + 1 <= _saveData.NextUnBeatenLevelIndex) return;
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
        if (_saveData == null) return;
        var levelIndex = PlayerPrefs.GetInt("levelIndex", 0);
        if (levelIndex != 0) _saveData.NextUnBeatenLevelIndex = levelIndex;


        var volume = PlayerPrefs.GetFloat("VolumeValue", -1f);
        if (volume != -1) OptionsLoaded?.Invoke(volume);

    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("levelIndex", _saveData.NextUnBeatenLevelIndex);
        PlayerPrefs.Save();
    }

    public int GetNextLevelIndex() => _saveData.NextUnBeatenLevelIndex;

    public void SetOptionsData(float volumeSliderValue)
    {
        PlayerPrefs.SetFloat("VolumeValue", volumeSliderValue);
    }

    public void SaveOptionsData()
    {
        PlayerPrefs.Save();
    }

    public void ResetSavedProgress()
    {
        _saveData.NextUnBeatenLevelIndex = 1;
        PlayerPrefs.SetInt("levelIndex", 0);
        PlayerPrefs.Save();
        DataReset?.Invoke();
    }

    public void SetVolume()
    {
        var volume = PlayerPrefs.GetFloat("VolumeValue", -1f);
        if (volume != -1) OptionsLoaded?.Invoke(volume);
    }

    public int GetClearedLevels()
    {
        return _saveData.NextUnBeatenLevelIndex;
    }
}