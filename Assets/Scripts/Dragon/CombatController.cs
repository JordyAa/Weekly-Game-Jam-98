using UnityEngine;

public class CombatController : MonoBehaviour
{
    [SerializeField] private GameObject fireballPrefab = null;
    
    [SerializeField] private float fireballCooldownMin = 1f;
    [SerializeField] private float fireballCooldownMax = 1f;
    private float fireballCooldown;
    private float fireballCooldownCounter;

    [SerializeField] private float bulletSpread = 90f;
    private int projectiles = 1;

    private void Start()
    {
        fireballCooldown = Random.Range(fireballCooldownMin, fireballCooldownMax);
        
        if (transform.CompareTag("Player"))
        {
            Achievements ac = FindObjectOfType<Achievements>();
            fireballCooldown *= ac.GetCooldownModifier();
            projectiles = ac.GetProjectiles();
        }
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
            
            for (int i = 1; i < projectiles + 1; i++)
            {
                float angleOffset = (float) i / (projectiles + 1) * bulletSpread - bulletSpread / 2f;

                Instantiate(fireballPrefab,
                        t.position + 0.3f * t.up,
                        t.rotation * Quaternion.Euler(0f, 0f, angleOffset))
                    .GetComponent<Fireball>().source = name;
            }

            if (transform.CompareTag("Player"))
            {
                Achievements.shotsFired++;
            }
        }
    }
}
