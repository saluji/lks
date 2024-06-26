using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] UIManager UIManager;
    [SerializeField] CursorManager CursorManager;
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
        UIManager.TogglePause(isPaused);
        CursorManager.ShowCursor();
    }
    public void Resume()
    {
        isPaused = false;
        CursorManager.HideCursor();
        UIManager.TogglePause(isPaused);
        Time.timeScale = 1;
    }
}
