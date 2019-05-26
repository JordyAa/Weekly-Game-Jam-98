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
    
    public int score { get; private set; }
    public int tailSize { get; private set; }
    public bool isUpgrading { get; private set; }
    
    private Head head;
    public Tail[] tails { get; private set; }

    public event Action<Dragon> OnGrowTail = delegate { };
    public event Action<Dragon> OnDestroyTail = delegate { };

    private void Start()
    {
        head = GetComponentInChildren<Head>();
        tails = new Tail[maxTailSize];
        
        for (int i = 0; i < startTailSize; i++)
        {
            AddTail(i);
        }
        
        EffectController.Init(this);
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
        for (int i = tailSize - 1; i >= stopAt; i--)
        {
            yield return new WaitForSeconds(waitTime);
            
            Destroy(tails[i].gameObject);
            tails[i] = null;
            
            score++;
            tailSize--;

            SpriteRenderer sr = tails[i]?.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sprite = endSprite;
            }

            OnDestroyTail(this);
        }

        isUpgrading = false;
    }
}