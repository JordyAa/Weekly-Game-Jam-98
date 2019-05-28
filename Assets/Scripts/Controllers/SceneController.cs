using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static bool isPaused { get; private set; }
    
    private Dragon player;
    private UIController ui;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Dragon>();
        ui = FindObjectOfType<UIController>();
        isPaused = false;
    }

    private void Update()
    {
        if (player.isDead) return;
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        ui.TogglePause();
    }

    public void LoadScene(int buildIndex)
    {
        isPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(buildIndex);
    }
}
