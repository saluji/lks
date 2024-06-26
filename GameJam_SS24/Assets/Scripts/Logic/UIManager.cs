using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;
    public void TogglePause(bool isPaused)
    {
        menuPanel.SetActive(isPaused);
    }
}