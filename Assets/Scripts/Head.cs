using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Head : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float rotateSpeed = 1f;
    
    [SerializeField] private float boostModifier = 1f;
    
    private float boost = 0f;
    private float rotate = 0f;
    
    private void Update()
    {
        rotate = Input.GetAxisRaw("Horizontal");
        boost = Input.GetAxisRaw("Vertical");
    }
    
    private void FixedUpdate()
    {
        if (Math.Abs(rotate) > Mathf.Epsilon)
        {
            transform.Rotate(0f, 0f, -rotate * rotateSpeed);
        }
        
        transform.Translate(0f, (movementSpeed + boost * boostModifier) * Time.deltaTime, 0f, Space.Self);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Tail"))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Edible"))
        {
            Player.instance.GrowTail();
        }
    }
}
