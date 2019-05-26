using UnityEngine;

public class Edible : MonoBehaviour
{
    [SerializeField] private GameObject consumeEffect = null;
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.parent.CompareTag("Player"))
        {
            GameObject.FindWithTag("Player").GetComponent<Dragon>().GrowTail();
            Instantiate(consumeEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
