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
    
    public Head head { get; private set; }
    public readonly List<Tail> tails = new List<Tail>();

    private bool isDestroying;
    
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

    public event Action<Dragon> OnGrowTail = delegate { };
    public event Action<Dragon> OnDestroyTail = delegate { };
    public event Action<Dragon> OnDeath = delegate { };
    
    private Coroutine destroyTailRoutine;

    private void Start()
    {
        EffectController.Init(this);
        head = GetComponentInChildren<Head>();

        OnGrowTail += AudioController.instance.PlayGrow;

        int size = Random.Range(startTailSizeMin, startTailSizeMax + 1);
        for (int i = 0; i < size; i++)
        {
            GrowTail(true);
        }
    }

    public void GrowTail(bool initial = false)
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
        go.transform.parent = transform;

        Tail tail = go.GetComponent<Tail>();
        tail.target = target;
        tails.Add(tail);

        Achievements.highScore = tails.Count;
        
        if (initial == false)
        {
            OnGrowTail(this);
        }
    }

    public void DestroyTail(int stopAt)
    {
        if (isDestroying)
        {
            StopCoroutine(destroyTailRoutine);
        }
        
        isDestroying = true;
        destroyTailRoutine = StartCoroutine(DestroyTailRoutine(Mathf.Max(0, stopAt)));
    }

    private IEnumerator DestroyTailRoutine(int stopAt)
    {
        stopAt = Mathf.Max(0, stopAt);
        
        for (int i = tails.Count - 1; i >= stopAt; i--)
        {
            yield return new WaitForSeconds(0.08f);
            
            tails[i].DropEdible(this);
            Destroy(tails[i].gameObject);
            tails.RemoveAt(i);

            if (i > 0)
            {
                tails[i - 1].GetComponent<SpriteRenderer>().sprite = endSprite;
            }

            OnDestroyTail(this);
        }
        
        CheckDeath();
        
        isDestroying = false;
    }

    private void CheckDeath()
    {
        if (tails.Count <= 0)
        {
            if (transform.name == "Player")
            {
                Achievements.totalDeaths++;
            }
            else
            {
                Achievements.totalKills++;
            }
            
            isDead = true;
            OnDeath(this);
        }
    }
}
