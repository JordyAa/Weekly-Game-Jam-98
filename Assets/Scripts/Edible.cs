using UnityEngine;

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

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.parent.CompareTag("Player"))
        {
            GameObject.Find("Player").GetComponent<Dragon>().GrowTail();
            Instantiate(consumeEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
