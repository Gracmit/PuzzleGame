using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{
    private int _levelIndex = 0;
    private TMP_Text _text;
    private Button _button;
    private Image _image;

    private void Awake()
    {
        _text = GetComponentInChildren<TMP_Text>();
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
    }

    public void LoadLevel()
    {

        if (_levelIndex == 0)
        {
            Debug.Log("No Level Index Set");
            return;
        }
        SceneManager.LoadScene(_levelIndex);
    }

    public void SetLevelIndex(int index)
    {
        _levelIndex = index;
        _text.SetText($"{index}");
    }

    public void SetColorAndInteractivity(Color color, bool openLevel)
    {
        _image.color = color;
        _button.interactable = openLevel;
        if(!openLevel) _text.color = Color.white;
    }
}
