using System.Collections;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    CursorManager cursorManager;
    UIManager uIManager;

    void Awake()
    {
        cursorManager = GameObject.Find("CursorManager").GetComponent<CursorManager>();
        uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

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
