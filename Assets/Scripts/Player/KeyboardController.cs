using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    private Head head;
    private Dragon player;
    private CombatController combat;

    private void Start()
    {
        head = GetComponentInChildren<Head>();
        player = GetComponent<Dragon>();
        combat = GetComponent<CombatController>();
    }

    private void Update()
    {
        if (player.isDead || SceneController.isPaused) return;
        
        head.rotate = Input.GetAxisRaw("Horizontal");
        head.boost = Input.GetAxisRaw("Vertical");
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            combat.ShootFireball(head.transform);
        }
    }
}
