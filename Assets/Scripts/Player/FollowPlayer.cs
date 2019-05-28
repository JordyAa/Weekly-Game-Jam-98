using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private float zPosition = 0f;
    
    private Dragon player;
    private Transform target;

    private void Start()
    {
        GameObject p = GameObject.Find("Player");
        player = p.GetComponent<Dragon>();
        target = p.transform.Find("Head");
    }

    private void Update()
    {
        if (player.isDead) return;
        
        Vector2 pos = target.position;
        transform.position = new Vector3(pos.x, pos.y, zPosition);
    }
}
