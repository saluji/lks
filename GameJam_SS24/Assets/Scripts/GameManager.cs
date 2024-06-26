using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] UIManager uIManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            uIManager.TogglePause();
        }
    }
}
