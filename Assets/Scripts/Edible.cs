using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Edible : MonoBehaviour
{
    [SerializeField] private float rotateSpeedMin = 0f;
    [SerializeField] private float rotateSpeedMax = 1f;
    private float rotate;
    
    [SerializeField] private GameObject consumeEffect = null;

    private void Start()
    {
        rotate = Random.Range(rotateSpeedMin, rotateSpeedMax);
    }

    private void Update()
    {
        transform.Rotate(0f, 0f, -rotate);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Transform t = other.transform.parent;
        if (t.CompareTag("Player"))
        {
            t.GetComponent<Dragon>().GrowTail();
            Instantiate(consumeEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D()
    {
        Destroy(gameObject);
    }
}
