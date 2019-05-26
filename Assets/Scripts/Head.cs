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

    private Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
            player.GrowTail();
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Tail"))
        {
            SceneManager.LoadScene(0);
        }
    }
}
