using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float lifeTime = 1f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        transform.Translate(0f, movementSpeed * Time.deltaTime, 0f, Space.Self);
    }
}
