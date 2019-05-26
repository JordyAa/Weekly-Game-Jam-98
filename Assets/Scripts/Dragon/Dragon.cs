using System;
using System.Collections;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    public int maxTailSize = 16;
    public int tailUpgradeSize = 10;
    [SerializeField] private int startTailSize = 1;

    [Header("Prefabs")]
    [SerializeField] private GameObject tailPrefab = null;
    [SerializeField] private Sprite tailSprite = null;
    [SerializeField] private Sprite endSprite = null;

    [Header("Effects")]
    public GameObject tailGrowEffect;
    public GameObject tailDestroyEffect;
    public GameObject deathEffect;
    
    public int score { get; private set; }
    public int tailSize { get; private set; }
    public bool isUpgrading { get; private set; }
    
    public Head head { get; private set; }
    public Tail[] tails { get; private set; }

    public event Action<Dragon> OnGrowTail = delegate { };
    public event Action<Dragon> OnDestroyTail = delegate { };
    public event Action<Dragon> OnDeath = delegate { };

    private bool _isDead;
    public bool isDead
    {
        get => _isDead;
        private set
        {
            if (value == false) throw new Exception("Can't bring back to life!");
            _isDead = true;
        }
    }

    private void Start()
    {
        head = GetComponentInChildren<Head>();
        tails = new Tail[maxTailSize];
        
        for (int i = 0; i < startTailSize; i++)
        {
            AddTail(i);
        }
        
        EffectController.Init(this);
        OnDestroyTail += CheckDeath;
    }

    
    
    private void AddTail(int index)
    {
        Transform target = index == 0 ? head.transform : tails[index - 1].transform;
        
        GameObject go = Instantiate(tailPrefab, target.position, Quaternion.identity);
        go.name = $"Tail ({tailSize})";
        go.transform.parent = transform;
        
        tails[tailSize] = go.GetComponent<Tail>();
        tails[tailSize].target = target;
        
        tailSize++;
    }

    public void GrowTail()
    {
        if (tailSize > 15) return;
        
        Transform target = tails[tailSize - 1].transform;
        Vector2 pos = target.position;
        
        tails[tailSize - 1].GetComponent<SpriteRenderer>().sprite = tailSprite;

        GameObject go = Instantiate(tailPrefab, pos, Quaternion.identity);
        go.name = $"Tail ({tailSize})";
        go.transform.parent = transform;
        
        tails[tailSize] = go.GetComponent<Tail>();
        tails[tailSize].target = target;
        
        tailSize++;

        OnGrowTail(this);
    }

    
    
    public void Upgrade()
    {
        if (tailSize > tailUpgradeSize)
        {
            DestroyTail(1);
        }
    }

    public void DestroyTail(int stopAt)
    {
        isUpgrading = true;
        StartCoroutine(DestroyTailRoutine(stopAt));
    }

    private IEnumerator DestroyTailRoutine(int stopAt, float waitTime = 0.1f)
    {
        for (int i = tailSize - 1; i >= stopAt && i >= 0; i--)
        {
            yield return new WaitForSeconds(waitTime);
            
            Destroy(tails[i].gameObject);
            tails[i] = null;

            if (i > 0)
            {
                tails[i - 1].GetComponent<SpriteRenderer>().sprite = endSprite;
            }
            
            score++;
            tailSize--;

            OnDestroyTail(this);
        }

        isUpgrading = false;
    }

    private void CheckDeath(Dragon dragon)
    {
        if (tailSize <= 0)
        {
            isDead = true;
            OnDeath(this);
        }
    }
}