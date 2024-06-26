using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] CursorManager cursorManager;
    [SerializeField] UIManager uIManager;
    private static bool isPaused = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isPaused)
        {
            Pause();
        }
    }
    void Pause()
    {
        isPaused = true;
        Time.timeScale = 0;
        uIManager.TogglePause(isPaused);
        cursorManager.ShowCursor();
    }
    public void Resume()
    {
        isPaused = false;
        cursorManager.HideCursor();
        uIManager.TogglePause(isPaused);
        Time.timeScale = 1;
    }
}
