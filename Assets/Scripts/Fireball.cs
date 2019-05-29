using System;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float lifeTime = 1f;

    private string _source = "";
    public string source
    {
        private get => _source;
        set
        {
            if (_source != "") throw new Exception("Source already defined!");
            _source = value;
        }
    }

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        transform.Translate(0f, movementSpeed * Time.deltaTime, 0f, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Transform t = other.transform.parent;
        if (t.name == source) return;
        
        if (other.CompareTag("Head") || other.CompareTag("Tail"))
        {
            t.GetComponent<Dragon>().DestroyTail(other.transform.GetSiblingIndex() - 1);
        }
        
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
