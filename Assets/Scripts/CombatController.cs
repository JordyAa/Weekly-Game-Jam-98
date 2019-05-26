using UnityEngine;

public class CombatController : MonoBehaviour
{
    [SerializeField] private GameObject fireballPrefab = null;
    [SerializeField] private float fireballCooldown = 1f;
    private float fireballCooldownCounter = 0f;

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
            Instantiate(fireballPrefab, t.position + 0.5f * t.up, t.rotation);
        }
    }
}
