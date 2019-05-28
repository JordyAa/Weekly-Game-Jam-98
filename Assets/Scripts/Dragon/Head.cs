using UnityEngine;
using Random = UnityEngine.Random;

public class Head : MonoBehaviour
{
    [SerializeField] private float movementSpeedMin = 1f;
    [SerializeField] private float movementSpeedMax = 1f;
    private float movementSpeed;
    
    [SerializeField] private float rotateSpeedMin = 1f;
    [SerializeField] private float rotateSpeedMax = 1f;
    private float rotateSpeed;
    
    [SerializeField] private float boostModifier = 1f;

    public float boost { private get; set; }
    public float rotate { private get; set; }

    private Dragon dragon;

    private void Start()
    {
        movementSpeed = Random.Range(movementSpeedMin, movementSpeedMax);
        rotateSpeed = Random.Range(rotateSpeedMin, rotateSpeedMax);

        if (transform.parent.CompareTag("Player"))
        {
            movementSpeed *= FindObjectOfType<Achievements>().GetMoveModifier();
        }
        
        dragon = transform.parent.GetComponent<Dragon>();
        dragon.OnDeath += DropEdible;
        dragon.OnDeath += Destroy;
    }

    private static void DropEdible(Dragon dragon)
    {
        if (dragon.dropOnDeath == null) return;

        Instantiate(dragon.dropOnDeath,
            dragon.head.transform.position,
            Quaternion.identity);
    }

    private static void Destroy(Dragon dragon)
    {
        GameObject.Find("SpawnController").GetComponent<SpawnController>().spawned--;
        Destroy(dragon.gameObject);
    }

    private void FixedUpdate()
    {
        if (dragon == null || dragon.isDead) return;
        
        transform.Rotate(0f, 0f, -rotate * rotateSpeed);
        transform.Translate(0f, (movementSpeed + boost * boostModifier) * Time.deltaTime, 0f, Space.Self);
    }
}
