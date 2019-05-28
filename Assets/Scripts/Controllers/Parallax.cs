using UnityEngine;
using UnityEngine.SceneManagement;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Vector3 moveDirection = Vector3.zero;
    [SerializeField] private Transform[] bgs = new Transform[0];

    private Transform cam;

    private void Start()
    {
        bgs = GetComponentsInChildren<Transform>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += FindCamera;
    }

    private void FindCamera(Scene scene, LoadSceneMode mode)
    {
        Camera c = Camera.main;
        if (c != null)
        {
            cam = c.transform;
        }
    }
    
    private void Update()
    {
        Vector2 camPos = cam.position;
        foreach (Transform bg in bgs)
        {
            bg.Translate(moveDirection * Time.deltaTime);
            
            Vector2 bgPos = bg.position;
            float yDiff = bgPos.y - camPos.y;
            if (yDiff < -24f)
            {
                bg.position += 40f * Vector3.up;
            }
            else if (yDiff > 24f)
            {
                bg.position += 40f * Vector3.down;
            }
            
            float xDiff = bgPos.x - camPos.x;
            if (xDiff > 20f)
            {
                bg.position += 40f * Vector3.left;
            }
            else if (xDiff < -20f)
            {
                bg.position += 40f * Vector3.right;
            }
        }
    }
}
