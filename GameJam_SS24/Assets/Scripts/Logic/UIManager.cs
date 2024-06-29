using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject hUDPanel;
    [SerializeField] GameObject gameOverPanel;

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