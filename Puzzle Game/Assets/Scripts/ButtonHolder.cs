using System.Collections.Generic;
using UnityEngine;

public class ButtonHolder : MonoBehaviour
{
    [SerializeField] private List<ButtonHolder> _linkedHolders;
    [SerializeField] private GameButton _targetButton;
    private GameButton _gameButton;

    public List<ButtonHolder> LinkedHolders => _linkedHolders;

    public bool HasGameButton => _gameButton != null;

    public bool HasCorrectButton => _gameButton == _targetButton;

    public void SetGameButton(GameButton gameButton)
    {
        _gameButton = gameButton;
    }
}
