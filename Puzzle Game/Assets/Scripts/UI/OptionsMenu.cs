using System;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private Slider _volumeSlider;

    private CanvasGroup _canvasGroup;


    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }

    private void Start()
    {
        GameManager.Instance.OptionsLoaded += HandleOptionsLoaded;
    }

    private void HandleOptionsLoaded(float volume)
    {
        _volumeSlider.value = volume;
    }

    public void VolumeChanged()
    {
        AudioManager.Instance.ChangeVolume(_volumeSlider.value);
        GameManager.Instance.SetOptionsData(_volumeSlider.value);
    }

    public void ResetSavedData()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
        MainMenuManager.Instance.OpenResetSavedDataMenu();
    }

    public void ShowCredits()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
        MainMenuManager.Instance.OpenCreditsMenu();
    }

    public void BackToMainMenu()
    {
        GameManager.Instance.SaveOptionsData();
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
        MainMenuManager.Instance.OpenMainMenu();
    }

    public void Open()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }
}
