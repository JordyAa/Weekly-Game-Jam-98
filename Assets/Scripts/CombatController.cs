using UnityEngine;

public class CombatController : MonoBehaviour
{
    [SerializeField] private GameObject fireballPrefab = null;
    [SerializeField] private float fireballCooldown = 1f;
    private bool fireballReady = true;

    public void ShootFireball(Transform t)
    {
        if (fireballReady)
        {
            fireballReady = false;
            Instantiate(fireballPrefab, t.position, t.rotation);
            Invoke(nameof(SetFireballReady), fireballCooldown);
        }
    }

    private void SetFireballReady()
    {
        fireballReady = true;
    }
}
