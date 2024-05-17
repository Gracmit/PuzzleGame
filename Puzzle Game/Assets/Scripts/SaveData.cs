using UnityEngine;

[CreateAssetMenu(menuName = "Create SaveData", fileName = "SaveData", order = 0)]
public class SaveData : ScriptableObject
{
    public int NextUnBeatenLevelIndex = 1;
}