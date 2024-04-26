using TMPro;
using UnityEngine;

public class MoveLimit : MonoBehaviour
{    
    [SerializeField] private TMP_Text _moveCounterText;
    private float _movesLeft;
    private bool _limitRunning;

    private void Awake()
    {
        _moveCounterText.enabled = false;
    }

    void Start()
    {
        FindObjectOfType<LevelManager>().StartMoveLimitCounter += HandleStartMoveLimitCounter;
        GameManager.Instance.Moved += HandleMoved;
        GameManager.Instance.Won += HandleWonState;
        GameManager.Instance.Lost += HandleLostState;
        
    }

    private void HandleLostState() => _limitRunning = false;

    private void HandleWonState() => _limitRunning = false;

    private void HandleMoved()
    {
        if (!_limitRunning) return;
        
        _movesLeft--;
        
        _moveCounterText.SetText(_movesLeft.ToString());

        if (_movesLeft <= 0)
        {
            GameManager.Instance.LevelFailed();
        }
    }

    private void HandleStartMoveLimitCounter(int limit)
    {
        _moveCounterText.enabled = true;
        _movesLeft = limit;
        _limitRunning = true;
        _moveCounterText.SetText(_movesLeft.ToString());
    }
}
