using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float fovRadius = 5f;
    [SerializeField] private float fovDistance = 10f;

    private Dragon player;
    private Transform target;

    private Head head;
    private CombatController combat;

    private void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        player = p.GetComponentInChildren<Dragon>();
        target = p.GetComponentInChildren<Head>().transform;
        player.OnDeath += Deregister;
        
        head = GetComponentInChildren<Head>();
        combat = GetComponent<CombatController>();
    }

    private void Update()
    {
        head.rotate = 0f;
        
        if (player.isDead || SceneController.isPaused) return;
        
        Vector3 facing = head.transform.up;
        Vector3 dir = target.position - transform.position;
        
        float angle = Mathf.Atan2(
                          Vector3.Dot(Vector3.forward, Vector3.Cross(dir, facing)),
                          Vector3.Dot(dir, facing)) *
                      Mathf.Rad2Deg;
        
        if (Mathf.Abs(angle) < fovRadius && Vector2.Distance(head.transform.position, target.position) < fovDistance)
        {
            combat.ShootFireball(head.transform);
        }

        head.rotate = angle > 0 ? 1f : -1f;
    }

    private static void Deregister(Dragon dragon)
    {
        GameObject.FindObjectOfType<SpawnController>().spawned -= 1;
    }
}
