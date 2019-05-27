using UnityEngine;

public class CombatController : MonoBehaviour
{
    [SerializeField] private GameObject fireballPrefab = null;
    
    [SerializeField] private float fireballCooldownMin = 1f;
    [SerializeField] private float fireballCooldownMax = 1f;
    private float fireballCooldown;
    private float fireballCooldownCounter;

    private void Start()
    {
        fireballCooldown = Random.Range(fireballCooldownMin, fireballCooldownMax);
    }

    private void Update()
    {
        if (fireballCooldownCounter > 0f)
        {
            fireballCooldownCounter -= Time.deltaTime;
        }
    }

    public void ShootFireball(Transform t)
    {
        if (fireballCooldownCounter <= 0f)
        {
            fireballCooldownCounter = fireballCooldown;
            Instantiate(fireballPrefab, t.position + 0.5f * t.up, t.rotation)
                .GetComponent<Fireball>().source = name;
        }
    }
}
