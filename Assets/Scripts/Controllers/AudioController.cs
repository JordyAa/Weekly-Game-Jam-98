using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance { get; private set; }

    [SerializeField] private AudioSource effectSource = null;
    
    [SerializeField] private AudioClip grow = null;
    [SerializeField] private AudioClip fireball = null;
    [SerializeField] private AudioClip explosion = null;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlayGrow(Dragon dragon)
    {
        effectSource.pitch = Random.Range(0.95f, 1.05f);
        effectSource.PlayOneShot(grow, 1f);
    }
    
    public void PlayFireball()
    {
        effectSource.pitch = Random.Range(0.95f, 1.05f);
        effectSource.PlayOneShot(fireball, 2f);
    }
    
    public void PlayExplosion()
    {
        effectSource.pitch = Random.Range(0.95f, 1.05f);
        effectSource.PlayOneShot(explosion, 0.7f);
    }
}
