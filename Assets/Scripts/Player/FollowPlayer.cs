using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private float zPosition = 0f;
    
    private Dragon player;
    private Transform target;

    private void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        player = p.GetComponent<Dragon>();
        target = p.GetComponentInChildren<Head>().transform;
    }

    private void Update()
    {
        if (player.isDead) return;
        
        Vector2 pos = target.position;
        transform.position = new Vector3(pos.x, pos.y, zPosition);
    }
}
