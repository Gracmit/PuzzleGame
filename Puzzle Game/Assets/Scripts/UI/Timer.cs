using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;
    private float _timer;
    private bool _timerRunning;
    
    private void Awake()
    {
        _timerText.enabled = false;
    }

    private void Start()
    {
        FindObjectOfType<LevelManager>().StartTimer += HandleTimerStart;
        GameManager.Instance.Won += HandleWonState;
    }


    void Update()
    {
        if (_timerRunning)
        {
            _timer -= Time.deltaTime;
            _timerText.SetText(Mathf.Ceil(_timer).ToString());

            if (_timer <= 0)
            {
                GameManager.Instance.LevelFailed();
                _timerRunning = false;
            }
        }
    }
    
    
    private void HandleTimerStart(int time)
    {
        _timer = time;
        _timerRunning = true;
        _timerText.enabled = true;
        _timerText.SetText(time.ToString());
    }

    private void HandleWonState()
    {
        _timerRunning = false;
    }
}
