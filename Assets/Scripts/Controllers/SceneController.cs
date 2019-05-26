using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static bool isPaused { get; private set; }
    
    private Dragon player;
    private UIController ui;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Dragon>();
        ui = FindObjectOfType<UIController>();

        isPaused = false;
    }

    private void Update()
    {
        if (player.isDead == false && Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
            ui.TogglePause();
        }
        
        if ((player.isDead || isPaused) && Input.GetKeyDown(KeyCode.Space))
        {
            isPaused = false;
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            
        }
    }

    private static void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
    }
}
