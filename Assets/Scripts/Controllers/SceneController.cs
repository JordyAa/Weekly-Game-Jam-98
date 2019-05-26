using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private Dragon player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Dragon>();
    }

    private void Update()
    {
        if (player.isDead && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
