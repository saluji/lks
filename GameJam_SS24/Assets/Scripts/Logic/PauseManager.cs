using UnityEngine;

public class PauseManager : MonoBehaviour
{
    CursorManager cursorManager;
    UIManager uIManager;
    public static bool isPaused = false;

    void Awake()
    {
        cursorManager = GameObject.Find("CursorManager").GetComponent<CursorManager>();
        uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

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
