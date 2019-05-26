using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float lifeTime = 1f;

    private float enableColliderCooldown = 0.25f;
    private bool colliderEnabled;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        if (colliderEnabled) return;
        
        enableColliderCooldown -= Time.deltaTime;
        
        if (enableColliderCooldown < 0f)
        {
            GetComponent<Collider2D>().enabled = true;
            colliderEnabled = true;
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(0f, movementSpeed * Time.deltaTime, 0f, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Head"))
        {
            other.transform.parent.GetComponent<Dragon>().DestroyTail(0);
        }
        else if (other.CompareTag("Tail"))
        {
            other.transform.parent.GetComponent<Dragon>().DestroyTail(other.transform.GetSiblingIndex() - 1);
        }
        
        Destroy(gameObject);
    }
}
