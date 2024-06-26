using UnityEngine;

public class CursorManager : MonoBehaviour
{
    void Awake()
    {
        HideCursor();
    }
    public void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
