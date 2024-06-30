using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    PauseManager pauseManager;
    int buildIndex;

    void Awake()
    {
        buildIndex = SceneManager.GetActiveScene().buildIndex;
        if (buildIndex != 0)
        {
            pauseManager = GameObject.Find("PauseManager").GetComponent<PauseManager>();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void TitelScreen()
    {
        SceneManager.LoadScene(buildIndex - 1);
    }

    public void Restart()
    {
        SceneManager.LoadScene(buildIndex);
        pauseManager.Resume();
    }
}
