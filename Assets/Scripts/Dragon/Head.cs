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

    private Dragon dragon;

    private void Start()
    {
        dragon = transform.parent.GetComponent<Dragon>();
        dragon.OnDeath += Destroy;
    }

    private static void Destroy(Dragon dragon)
    {
        Destroy(dragon.gameObject);
    }

    private void FixedUpdate()
    {
        if (dragon.isDead) return;
        
        if (Math.Abs(rotate) > Mathf.Epsilon)
        {
            transform.Rotate(0f, 0f, -rotate * rotateSpeed);
        }
        
        transform.Translate(0f, (movementSpeed + boost * boostModifier) * Time.deltaTime, 0f, Space.Self);
    }

    private void OnDestroy()
    {
        if (dragon.dropOnDeath != null)
        {
            Instantiate(dragon.dropOnDeath,
                dragon.head.transform.position,
                Quaternion.identity);
        }
    }
}
