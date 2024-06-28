using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject hUDPanel;

    public void TogglePause(bool isPaused)
    {
        menuPanel.SetActive(isPaused);
        hUDPanel.SetActive(!isPaused);
    }
}