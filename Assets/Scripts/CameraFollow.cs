using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float followSpeed;
    
    private Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Head").transform;
    }

    private void FixedUpdate()
    {
        Vector2 newPos = Vector2.MoveTowards(transform.position, target.position, followSpeed);
        transform.position = new Vector3(newPos.x, newPos.y, -10f);
    }
}
