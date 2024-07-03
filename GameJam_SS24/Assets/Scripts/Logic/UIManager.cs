using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    PlayerStateMachine player;
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject hUDPanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject interactPanel;
    [SerializeField] Slider playerHP;
    [SerializeField] Slider wifeyHP;
    [SerializeField] TextMeshProUGUI scoreText;

    public Slider PlayerHP { get { return playerHP; } }
    public Slider WifeyHP { get { return wifeyHP; } }

    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerStateMachine>();
        menuPanel.SetActive(false);
        hUDPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        interactPanel.SetActive(false);

        scoreText.text = "";
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
    public void UpdateScore()
    {
        scoreText.text = "";
        scoreText.text = player.SnatchCounter.ToString() + " / 100 ";
    }
}