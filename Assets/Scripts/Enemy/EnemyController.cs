using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float fovRadius = 5f;
    [SerializeField] private float fovDistance = 10f;
    [SerializeField] private float maxDistance = 30f;

    // Player components.
    private Dragon player;
    private Transform target;

    // This dragon's components.
    private Head head;
    private CombatController combat;

    private void Start()
    {
        GameObject p = GameObject.Find("Player");
        player = p.GetComponent<Dragon>();
        target = p.transform.Find("Head");

        head = GetComponentInChildren<Head>();
        combat = GetComponent<CombatController>();
        
        player.OnDeath += Deregister;
    }

    private void Update()
    {
        if (player.isDead || SceneController.isPaused)
        {
            head.rotate = 0f;
            return;
        }

        Transform t = head.transform;
        Vector3 tPos = t.position;
        Vector3 facing = t.up;
        Vector3 targetPos = target.position;
        Vector3 dir = targetPos - tPos;

        float distance = Vector2.Distance(tPos, targetPos);
        if (distance > maxDistance)
        {
            Deregister();
            Destroy(gameObject);
        }
        
        float angle = Mathf.Atan2(Vector3.Dot(Vector3.forward, Vector3.Cross(dir, facing)), Vector3.Dot(dir, facing))*
                      Mathf.Rad2Deg;
        
        if (Mathf.Abs(angle) < fovRadius && distance < fovDistance)
        {
            combat.ShootFireball(t);
        }

        head.rotate = angle > 0 ? 1f : -1f;
    }

    private static void Deregister(Dragon dragon = null)
    {
        FindObjectOfType<SpawnController>().spawned--;
    }
}
