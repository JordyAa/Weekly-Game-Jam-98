using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float movementSpeedMin = 0f;
    [SerializeField] private float movementSpeedMax = 1f;
    private float movementSpeed;

    [SerializeField] private float rotateSpeedMin = 0f;
    [SerializeField] private float rotateSpeedMax = 1f;
    private float rotate;

    [SerializeField] private float maxDistance = 30f;

    [SerializeField] private Vector3 direction = Vector3.left;

    [SerializeField] private float scaleMin = 1f;
    [SerializeField] private float scaleMax = 1f;
    
    private Dragon player;
    private Transform target;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Dragon>();
        target = player.transform.GetChild(0);
        
        movementSpeed = Random.Range(movementSpeedMin, movementSpeedMax);
        rotate = Random.Range(rotateSpeedMin, rotateSpeedMax);

        transform.localScale = Random.Range(scaleMin, scaleMax) * Vector3.one;
    }

    private void Update()
    {
        if (player.isDead) return;
        
        float distance = Vector2.Distance(transform.position, target.position);
        if (distance > maxDistance)
        {
            FindObjectOfType<SpawnController>().spawned--;
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        transform.Rotate(0f, 0f, -rotate);
        transform.Translate(movementSpeed * Time.deltaTime * direction, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Head") || other.CompareTag("Tail"))
        {
            Transform t = other.transform.parent;
            t.GetComponent<Dragon>().DestroyTail(other.transform.GetSiblingIndex() - 1);
        }
    }
}
