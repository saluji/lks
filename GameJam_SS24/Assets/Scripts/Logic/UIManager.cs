using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject hUDPanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] Slider playerHP;
    [SerializeField] Slider wifeyHP;
    [SerializeField] TextMeshProUGUI scoreComponent;

    public Slider PlayerHP { get { return playerHP; } }
    public Slider WifeyHP { get { return wifeyHP; } }

    void Awake()
    {
        menuPanel.SetActive(false);
        hUDPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        scoreComponent.text = string.Empty;
        scoreComponent.text = "0";
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
    
    public void UpdateScore(int counter)
    {
        scoreComponent.text = string.Empty;
        scoreComponent.text = counter.ToString();
    }
}