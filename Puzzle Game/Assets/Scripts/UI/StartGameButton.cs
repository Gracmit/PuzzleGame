using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
        public void StartGame()
        {
                SceneManager.LoadScene(GameManager.Instance.GetNextLevelIndex());
        }
}
