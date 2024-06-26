using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private static bool isPaused = false;
    [SerializeField] UIManager uIManager;
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
    }
    public void Resume()
    {
        isPaused = false;
        uIManager.TogglePause(isPaused);
        Time.timeScale = 1;
    }
}
