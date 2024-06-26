using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private static bool isPaused = false;
    [SerializeField] UIManager UIManager;
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
    }
    public void Resume()
    {
        isPaused = false;
        UIManager.TogglePause(isPaused);
        Time.timeScale = 1;
    }
}
