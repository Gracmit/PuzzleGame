using UnityEngine;
using UnityEngine.EventSystems;

public class GameButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private ButtonHolder _holder;
    private bool _moving;
    private Vector3 _startingPosition;
    private Vector3 _positionToMove;
    private float _moveTimer;

    private void Awake()
    {
        _holder.SetGameButton(this);
        transform.position = _holder.transform.position;
    }


    private void Update()
    {
        if (_moving)
        {
            _moveTimer += Time.deltaTime * 2;
            if (_moveTimer > 1f)
            {
                _moveTimer = 1;
                _moving = false;
                GameManager.Instance.PieceStopped();
            }
            transform.position = Vector3.Lerp(_startingPosition, _positionToMove, _moveTimer);
        }
    }

    private void TryToMove()
    {
        if (_moving)
        {
            return;
        }
        
        ButtonHolder movableHolder = null;
        foreach (var linkedHolder in _holder.LinkedHolders)
        {
            if (!linkedHolder.HasGameButton)
            {
                movableHolder = linkedHolder;
                break;
            }
        }

        if (movableHolder != null)
        {
            GameManager.Instance.PieceMoving();
            _moving = true;
            _moveTimer = 0;
            _startingPosition = transform.position;
            _holder.SetGameButton(null);
            _holder = movableHolder;
            _positionToMove = _holder.transform.position;
            _holder.SetGameButton(this);
            AudioManager.Instance.PlayMoveSFX();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        TryToMove();
    }
}
