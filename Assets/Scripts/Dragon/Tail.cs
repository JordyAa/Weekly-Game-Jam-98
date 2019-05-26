using UnityEngine;

public class Tail : MonoBehaviour
{
    [SerializeField] private float edibleSpawnChanceMin = 0f;
    [SerializeField] private float edibleSpawnChanceMax = 1f;
    
    private bool hasTarget;
    private Transform _target;
    public Transform target
    {
        set
        {
            _target = value;
            GetComponent<DistanceJoint2D>().connectedBody = _target.GetComponent<Rigidbody2D>();
            hasTarget = true;
        }
    }

    private void Update()
    {
        if (hasTarget == false) return;

        Transform t = transform;
        Vector3 pos = t.position;
        Vector3 targetPos = _target.position;

        t.right = targetPos - pos;

        float angle = Mathf.Atan2(targetPos.y - pos.y, targetPos.x - pos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f + angle));
    }

    private void OnDestroy()
    {
        Transform t = transform;
        Dragon d = t.parent.GetComponent<Dragon>();
        
        if (d.dropOnDeath != null && Random.Range(edibleSpawnChanceMin, edibleSpawnChanceMax) > 0.5f)
        {
            Instantiate(d.dropOnDeath,
                t.position,
                Quaternion.identity);
        }
    }
}
