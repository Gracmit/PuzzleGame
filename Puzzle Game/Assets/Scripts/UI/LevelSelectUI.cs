using System.Collections.Generic;
using UnityEngine;

public class LevelSelectUI : MonoBehaviour
{
    [SerializeField] private Transform _contentContainer;
    [SerializeField] private GameObject _buttonPrefab;
    [SerializeField] private int LevelCount;
    [SerializeField] private List<Color> _colors;
    [SerializeField] private Color _lockedColor;
    private CanvasGroup _canvasGroup;
    private readonly List<LevelSelectButton> _buttons = new List<LevelSelectButton>();

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }

    private void Start()
    {
        GameManager.Instance.DataReset += HandleDataReset;
        PopulateContent();
    }

    private void HandleDataReset()
    {
        for (int i = 1; i < _buttons.Count; i++)
        {
            _buttons[i].SetColorAndInteractivity(_lockedColor, false);
        }
    }

    private void PopulateContent()
    {
        var clearedLevels = GameManager.Instance.GetClearedLevels();
        
        for (int i = 0; i < LevelCount; i++)
        {
            var instance = Instantiate(_buttonPrefab, _contentContainer);
            var button = instance.GetComponent<LevelSelectButton>();
            _buttons.Add(button);
            button.SetLevelIndex(i +1 );
            bool openLevel = clearedLevels > i;
            var color = _colors[i % _colors.Count];
            if (!openLevel) color = _lockedColor;
            button.SetColorAndInteractivity(color, openLevel);
        }
    }

    public void Open()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }

    public void BackToMainMenu()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
        MainMenuManager.Instance.OpenMainMenu();
    }
}
