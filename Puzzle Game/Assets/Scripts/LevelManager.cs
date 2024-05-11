using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject _linePrefab;
    [SerializeField] private bool _timerLevel;
    [SerializeField] private int _levelTime;
    [SerializeField] private bool _moveLimitLevel;
    [SerializeField] private int _moveLimit;
    
    
    public event Action<int> StartTimer;
    public event Action<int> StartMoveLimitCounter;

    private List<Connection> _connections;

    [Serializable]
    private struct Connection
    {
        public ButtonHolder StartingHolder;
        public ButtonHolder EndingHolder;

        public Connection(ButtonHolder startingHolder, ButtonHolder endingHolder)
        {
            StartingHolder = startingHolder;
            EndingHolder = endingHolder;
        }
    }

    void Start()
    {
        GameManager.Instance.NewLevelLoaded();
        CreateLinesBetweenConnectedButtonHolders();
        if (_timerLevel) StartCoroutine(WaitToStartTheTimer());
        if (_moveLimitLevel) StartCoroutine(WaitToStartTheLimitCounter());
    }

    private IEnumerator WaitToStartTheLimitCounter()
    {
        yield return null;
        StartMoveLimitCounter?.Invoke(_moveLimit);
    }

    private IEnumerator WaitToStartTheTimer()
    {
        yield return null;
        StartTimer?.Invoke(_levelTime);
        
    }

    private void CreateLinesBetweenConnectedButtonHolders()
    {
        FindConnections();
        DrawLines();
    }

    private void DrawLines()
    {
        foreach (var connection in _connections)
        {
            var vectorToTarget = connection.EndingHolder.transform.position -
                                 connection.StartingHolder.transform.position;
            var angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            var instance = Instantiate(_linePrefab, connection.StartingHolder.transform.position, rotation);

            var distance = Vector3.Distance(connection.EndingHolder.transform.position, connection.StartingHolder.transform.position);
            instance.transform.position = Vector3.Lerp(connection.EndingHolder.transform.position, connection.StartingHolder.transform.position, 0.5f);
            var spriteRenderer = instance.GetComponent<SpriteRenderer>();
            spriteRenderer.size = new Vector2(distance - .9f, 0.15f);
        }
    }

    private void FindConnections()
    {
        var holders = FindObjectsOfType<ButtonHolder>();
        _connections = new List<Connection>();
        foreach (var holder in holders)
        {
            foreach (var linkedHolder in holder.LinkedHolders)
            {
                if (!IsConnectionAdded(holder, linkedHolder))
                {
                    _connections.Add(new Connection(holder, linkedHolder));
                }
            }
        }
    }

    private bool IsConnectionAdded(ButtonHolder holder, ButtonHolder linkedHolder)
    {
        foreach (var connection in _connections)
        {
            if (connection.EndingHolder == holder)
            {
                if (connection.StartingHolder == linkedHolder) return true;
            }
        }

        return false;
    }
}
