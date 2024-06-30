using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject hUDPanel;
    [SerializeField] GameObject gameOverPanel;

    void Awake()
    {
        // menuPanel = GameObject.Find("MenuPanel");
        // hUDPanel = GameObject.Find("HUDPanel");
        // gameOverPanel = GameObject.Find("GameOverPanel");

        menuPanel.SetActive(false);
        hUDPanel.SetActive(true);
        gameOverPanel.SetActive(false);
    }

    public void TogglePause(bool isPaused)
    {
        menuPanel.SetActive(isPaused);
        hUDPanel.SetActive(!isPaused);
    }

    public void ToggleGameOver()
    {
        hUDPanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }
}