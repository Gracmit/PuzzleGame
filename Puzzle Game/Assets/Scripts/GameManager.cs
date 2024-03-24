using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => _instance;
    public event Action Won;
    public event Action Lost;
    public event Action Moved;

    private static GameManager _instance;
    private ButtonHolder[] _buttonHolders;
    private int _piecesMoving;


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
    }


    private void FindButtonHolders()
    {
        _buttonHolders = FindObjectsOfType<ButtonHolder>();
    }

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
        }
    }

    public void PieceMoving()
    {
        _piecesMoving++;
    }

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
    }

    public void NewLevelLoaded()
    {
        FindButtonHolders();
    }
}
