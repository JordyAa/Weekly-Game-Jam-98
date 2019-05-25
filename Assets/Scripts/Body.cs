using UnityEngine;

public class Body : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Edible"))
        {
            Player.instance.AddTail();
            Destroy(other.gameObject);
        }
    }
}
