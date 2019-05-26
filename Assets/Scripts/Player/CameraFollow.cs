using UnityEngine;

public class CameraFollow : MonoBehaviour
{
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
        transform.position = new Vector3(pos.x, pos.y, -10f);
    }
}
