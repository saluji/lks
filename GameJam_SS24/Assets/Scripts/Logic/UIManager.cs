using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject MenuPanel;
    public void TogglePause(bool isPaused)
    {
        MenuPanel.SetActive(isPaused);
    }
}