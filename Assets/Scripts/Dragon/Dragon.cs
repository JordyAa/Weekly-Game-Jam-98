using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dragon : MonoBehaviour
{
    [SerializeField] private int startTailSizeMin = 1;
    [SerializeField] private int startTailSizeMax = 10;

    [Header("Prefabs")]
    [SerializeField] private GameObject tailPrefab = null;
    [SerializeField] private Sprite tailSprite = null;
    [SerializeField] private Sprite endSprite = null;

    [Header("Effects")]
    public GameObject tailGrowEffect;
    public GameObject tailDestroyEffect;
    public GameObject deathEffect;
    public GameObject dropOnDeath;
    
    private bool isDestroying { get; set; }
    
    public Head head { get; private set; }
    public List<Tail> tails = new List<Tail>();

    public event Action<Dragon> OnGrowTail = delegate { };
    public event Action<Dragon> OnDestroyTail = delegate { };
    public event Action<Dragon> OnDeath = delegate { };

    private Coroutine destroyTailRoutine;
    
    private bool _isDead;
    public bool isDead
    {
        get => _isDead;
        private set
        {
            if (value == false) throw new Exception("Can't bring dragon back to life!");
            _isDead = true;
        }
    }

    private void Start()
    {
        EffectController.Init(this);
        head = GetComponentInChildren<Head>();

        for (int i = 0; i < Random.Range(startTailSizeMin, startTailSizeMax + 1); i++)
        {
            GrowTail();
        }
    }

    public void GrowTail()
    {
        Transform target;
        if (tails.Count == 0)
        {
            target = head.transform;
        }
        else
        {
            target = tails[tails.Count - 1].transform;
            tails[tails.Count - 1].GetComponent<SpriteRenderer>().sprite = tailSprite;
        }
        
        GameObject go = Instantiate(tailPrefab, target.position, Quaternion.identity);
        go.name = $"Tail ({tails.Count})";
        go.transform.parent = transform;

        Tail tail = go.GetComponent<Tail>();
        tail.target = target;
        tails.Add(tail);

        OnGrowTail(this);
    }

    public void DestroyTail(int stopAt)
    {
        if (isDestroying)
        {
            StopCoroutine(destroyTailRoutine);
        }
        
        isDestroying = true;
        destroyTailRoutine = StartCoroutine(DestroyTailRoutine(stopAt));
    }

    private IEnumerator DestroyTailRoutine(int stopAt)
    {
        for (int i = tails.Count - 1; i >= stopAt && i >= 0; i--)
        {
            yield return new WaitForSeconds(0.1f);
            
            Destroy(tails[i].gameObject);
            tails.RemoveAt(i);

            if (i > 0)
            {
                tails[i - 1].GetComponent<SpriteRenderer>().sprite = endSprite;
            }

            OnDestroyTail(this);
        }
        
        if (tails.Count <= 0)
        {
            isDead = true;
            OnDeath(this);
        }
        
        isDestroying = false;
        
    }
}
