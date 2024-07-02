using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject hUDPanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject interactPanel;

    void Awake()
    {
        menuPanel.SetActive(false);
        hUDPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        interactPanel.SetActive(false);
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
    public void ShowInteractPanel()
    {
        interactPanel.SetActive(true);
    }
}