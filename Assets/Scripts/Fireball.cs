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
        if (other.transform.parent.name == source) return;
        
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
