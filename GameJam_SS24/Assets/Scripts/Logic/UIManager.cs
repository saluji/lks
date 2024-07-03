using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject hUDPanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject interactPanel;
    [SerializeField] Slider playerHP;
    [SerializeField] Slider wifeyHP;

    public Slider PlayerHP { get { return playerHP; } }
    public Slider WifeyHP { get { return wifeyHP; } }
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
    public void HideInteractPanel()
    {
        interactPanel.SetActive(false);
    }
}