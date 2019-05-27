using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = System.Diagnostics.Debug;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Vector3 moveDirection = Vector3.zero;
    [SerializeField] private Transform[] bgs = new Transform[0];

    private Transform cam;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += FindCamera;
    }

    private void FindCamera(Scene scene, LoadSceneMode mode)
    {
        Debug.Assert(Camera.main != null, "Camera.main != null");
        cam = Camera.main.transform;
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
