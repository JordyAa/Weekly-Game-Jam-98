using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Head : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float rotateSpeed = 1f;
    [SerializeField] private float boostModifier = 1f;
    
    public float boost { private get; set; }
    public float rotate { private get; set; }

    private Dragon dragon;

    private void Start()
    {
        dragon = transform.parent.GetComponent<Dragon>();
    }

    private void FixedUpdate()
    {
        if (Math.Abs(rotate) > Mathf.Epsilon)
        {
            transform.Rotate(0f, 0f, -rotate * rotateSpeed);
        }
        
        transform.Translate(0f, (movementSpeed + boost * boostModifier) * Time.deltaTime, 0f, Space.Self);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Edible"))
        {
            dragon.GrowTail();
        }
    }
}
