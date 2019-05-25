using System;
using UnityEngine;

public class Head : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float rotateSpeed = 1f;
    
    private void Update()
    {
        float rotate = Input.GetAxisRaw("Horizontal");
        if (Math.Abs(rotate) > Mathf.Epsilon)
        {
            transform.Rotate(0, 0, -rotate * rotateSpeed);
        }
    }
    
    private void FixedUpdate()
    {
        transform.Translate(0f, movementSpeed * Time.deltaTime, 0f, Space.Self);
    }
}
