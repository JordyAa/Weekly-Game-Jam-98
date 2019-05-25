using System.Collections;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance { get; private set; }
    
    [Header("Prefabs")]
    [SerializeField] private GameObject tailPrefab = null;
    [SerializeField] private GameObject tailGrowEffect = null;

    [Header("Sprites")]
    [SerializeField] private Sprite tailSprite = null;
    [SerializeField] private Sprite endSprite = null;
    
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI sizeText = null;
    [SerializeField] private TextMeshProUGUI scoreText = null;
    [SerializeField] private GameObject upgradeText = null;

    private int score = 0;
    private int tailSize = 1;
    private bool isUpgrading = false;
    
    private Head head = null;
    private readonly Tail[] tails = new Tail[16];

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        head = GameObject.FindGameObjectWithTag("Head").GetComponent<Head>();
        
        tails[0] = GameObject.FindGameObjectWithTag("Tail").GetComponent<Tail>();
        tails[0].target = head.transform;
    }
    
    private void Update()
    {
        if (isUpgrading == false && tailSize < 16 && Input.GetKeyDown(KeyCode.E))
        {
            GrowTail();
        }

        if (isUpgrading == false && tailSize > 9 && Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(DestroyTail());
        }
    }

    public void GrowTail()
    {
        Transform target = tails[tailSize - 1].transform;
        Vector2 pos = target.position;
        
        tails[tailSize - 1].GetComponent<SpriteRenderer>().sprite = tailSprite;
        Instantiate(tailGrowEffect, pos, Quaternion.identity);
        
        GameObject go = Instantiate(tailPrefab, pos, Quaternion.identity);
        go.name = $"Tail ({tailSize})";
        go.transform.parent = transform;
        
        Tail tail = go.GetComponent<Tail>();
        tail.target = target;
        tails[tailSize] = tail;

        sizeText.text = $"SIZE: {tailSize} / 15";
        upgradeText.SetActive(tailSize > 9);
        tailSize++;
    }

    private IEnumerator DestroyTail(float waitTime = 0.1f)
    {
        isUpgrading = true;
        upgradeText.SetActive(false);
        
        for (int i = tailSize - 1; i > 0; i--)
        {
            yield return new WaitForSeconds(waitTime);
            
            Instantiate(tailGrowEffect, tails[i].transform.position, Quaternion.identity);
            Destroy(tails[i].gameObject);
            tails[i] = null;
            
            tails[i - 1].GetComponent<SpriteRenderer>().sprite = endSprite;

            tailSize--;
            score++;
            sizeText.text = $"SIZE: {tailSize - 1} / 15";
            scoreText.text = "SCORE: " + score;
        }

        isUpgrading = false;
    }
}