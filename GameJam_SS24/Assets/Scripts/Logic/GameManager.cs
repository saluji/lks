using UnityEngine;
public class GameManager : MonoBehaviour
{
    [SerializeField] CursorManager cursorManager;
    [SerializeField] UIManager uIManager;

    public void GameOver()
    {
        PauseManager.isPaused = true;
        Time.timeScale = 0;
        uIManager.ToggleGameOver();
        cursorManager.ShowCursor();
    }
}
