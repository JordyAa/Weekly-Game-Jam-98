using UnityEngine;

public class InputController : MonoBehaviour
{
    private Head head;
    private Player player;
    private CombatController combat;
    
    private void Start()
    {
        head = GameObject.FindGameObjectWithTag("Head").GetComponent<Head>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        combat = GameObject.FindGameObjectWithTag("Player").GetComponent<CombatController>();
    }

    private void Update()
    {
        UpdateHead();
        UpdatePlayerController();
        UpdateCombatController();
    }

    private void UpdateHead()
    {
        head.rotate = Input.GetAxisRaw("Horizontal");
        head.boost = Input.GetAxisRaw("Vertical");
    }

    private void UpdatePlayerController()
    {
        if (player.isUpgrading) return;
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            player.GrowTail();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            player.DestroyTail();
        }
    }

    private void UpdateCombatController()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            combat.ShootFireball(head.transform);
        }
    }
}
