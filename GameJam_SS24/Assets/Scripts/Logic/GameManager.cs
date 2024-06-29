using System.Collections;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    [SerializeField] CursorManager cursorManager;
    [SerializeField] UIManager uIManager;
    float gameOverLength = 4;

    public void GameOver()
    {
        StartCoroutine(GameOverCountdown());
        PauseManager.isPaused = true;
        Time.timeScale = 0;
        uIManager.ToggleGameOver();
        cursorManager.ShowCursor();
    }

    IEnumerator GameOverCountdown()
    {
        yield return new WaitForSeconds(4);
    }
}
