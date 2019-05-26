using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject tailPrefab = null;
    [SerializeField] private Sprite tailSprite = null;
    [SerializeField] private Sprite endSprite = null;

    public int score { get; private set; }
    public int tailSize { get; private set; }
    public bool isUpgrading { get; private set; }
    
    private Head head;
    public Tail[] tails { get; private set; }

    public static event Action<Player> OnGrowTail = delegate { };
    public static event Action<Player> OnDestroyTail = delegate { };

    private void Start()
    {
        head = GameObject.FindGameObjectWithTag("Head").GetComponent<Head>();
        
        tails = new Tail[16];
        tails[0] = GameObject.FindGameObjectWithTag("Tail").GetComponent<Tail>();
        tails[0].target = head.transform;
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

    public void DestroyTail()
    {
        if (tailSize > 10)
        {
            isUpgrading = true;
            StartCoroutine(DestroyTailRoutine());
        }
    }
    
    private IEnumerator DestroyTailRoutine(float waitTime = 0.1f)
    {
        for (int i = tailSize - 1; i > 0; i--)
        {
            yield return new WaitForSeconds(waitTime);
            
            Destroy(tails[i].gameObject);
            tails[i] = null;
            
            tails[i - 1].GetComponent<SpriteRenderer>().sprite = endSprite;

            score++;
            tailSize--;

            OnDestroyTail(this);
        }

        isUpgrading = false;
    }
}