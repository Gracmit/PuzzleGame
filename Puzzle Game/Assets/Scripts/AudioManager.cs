using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip _victorySfx;
    [SerializeField] private AudioClip _loseSfx;
    [SerializeField] private AudioClip _moveSfx;
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioSource _musicSource;

    private static AudioManager _instance;
    public static AudioManager Instance => _instance;
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        GameManager.Instance.OptionsLoaded += HandleOptionsLoaded;
    }

    private void HandleOptionsLoaded(float volume)
    {
        ChangeVolume(volume);
    }

    public void PlayVictorySFX()
    {
        _sfxSource.clip = _victorySfx;
        _sfxSource.Play();
        
    }
    
    public void PlayLoseSFX()
    {
        _sfxSource.clip = _loseSfx;
        _sfxSource.Play();
    }

    public void PlayMoveSFX()
    {
        _sfxSource.clip = _moveSfx;
        _sfxSource.Play();
    }

    public void ChangeVolume(float volumeSliderValue)
    {
        _musicSource.volume = volumeSliderValue;
        _sfxSource.volume = volumeSliderValue;
    }
}
