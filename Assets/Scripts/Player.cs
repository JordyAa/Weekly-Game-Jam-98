using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance { get; private set; }
    
    [SerializeField] private GameObject tailPrefab;

    private Head head;
    private List<Tail> tails;

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
        tails = new List<Tail>();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            AddTail();
        }
    }

    public void AddTail()
    {
        int tailLength = tails.Count;
        Transform target = tailLength == 0 ? head.transform : tails[tailLength - 1].transform;
        
        GameObject go = Instantiate(tailPrefab, target.position, Quaternion.identity);
        
        Tail tail = go.GetComponent<Tail>();
        tail.target = target;
        tails.Add(tail);
    }
}