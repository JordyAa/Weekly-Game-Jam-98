using UnityEngine;

public class InputController : MonoBehaviour
{
    private Head playerHead;
    private Player player;
    
    private void Start()
    {
        playerHead = GameObject.FindGameObjectWithTag("Head").GetComponent<Head>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        UpdateHead();
        UpdatePlayerController();
    }

    private void UpdateHead()
    {
        playerHead.rotate = Input.GetAxisRaw("Horizontal");
        playerHead.boost = Input.GetAxisRaw("Vertical");
    }

    private void UpdatePlayerController()
    {
        if (player.isUpgrading) return;
        
        if (player.tailSize < 16 && Input.GetKeyDown(KeyCode.E))
        {
            player.GrowTail();
        }

        if (player.tailSize > 10 && Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(player.DestroyTail());
        }
    }
}
