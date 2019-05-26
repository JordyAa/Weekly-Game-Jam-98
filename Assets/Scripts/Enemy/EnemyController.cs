using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float fovRadius = 1f;

    private Dragon player;
    private Transform target;

    private Head head;
    private CombatController combat;

    private void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        player = p.GetComponentInChildren<Dragon>();
        target = p.GetComponentInChildren<Head>().transform;
        
        head = GetComponentInChildren<Head>();
        combat = GetComponent<CombatController>();
    }

    private void Update()
    {
        if (player.isDead) return;
        
        Vector3 facing = head.transform.up;
        Vector3 dir = target.position - transform.position;
        
        float angle = Mathf.Atan2(
                          Vector3.Dot(Vector3.forward, Vector3.Cross(dir, facing)),
                          Vector3.Dot(dir, facing)) *
                      Mathf.Rad2Deg;
        
        if (Mathf.Abs(angle) < fovRadius)
        {
            combat.ShootFireball(head.transform);
        }

        head.rotate = angle > 0 ? 1f : -1f;
    }
}
