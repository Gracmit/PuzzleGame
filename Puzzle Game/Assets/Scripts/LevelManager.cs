using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject _linePrefab;
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
        CreateLinesBetweenConnectedButtonHolders();
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
            instance.transform.localScale = new Vector3(distance, 0.15f, 1);
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
