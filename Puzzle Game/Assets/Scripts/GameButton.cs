using UnityEngine;
using UnityEngine.EventSystems;

public class GameButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private ButtonHolder _holder;

    private void Awake()
    {
        _holder.SetGameButton(this);
        transform.position = _holder.transform.position;
    }

    private void TryToMove()
    {
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
            _holder.SetGameButton(null);
            _holder = movableHolder;
            _holder.SetGameButton(this);
            transform.position = _holder.transform.position;
            GameManager.Instance.CheckForVictoryCondition();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Hit button");
        TryToMove();
    }
}
