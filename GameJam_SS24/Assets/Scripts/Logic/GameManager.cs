using System.Collections;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    [SerializeField] CursorManager cursorManager;
    [SerializeField] UIManager uIManager;

    void GameOver()
    {
        PauseManager.isPaused = true;
        Time.timeScale = 0;
        uIManager.ToggleGameOver();
        cursorManager.ShowCursor();
    }

    public IEnumerator GameOverCountdown()
    {
        yield return new WaitForSeconds(3);
        GameOver();
    }
}
