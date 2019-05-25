using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Head").transform;
    }

    private void Update()
    {
        Vector2 pos = target.position;
        transform.position = new Vector3(pos.x, pos.y, -10f);
    }
}
