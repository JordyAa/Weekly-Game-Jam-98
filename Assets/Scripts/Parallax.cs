using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Vector3 moveDirection;
    [SerializeField] private Transform[] bgs;

    private Transform cam;
    private const float camTop = 6f;
    private const float camRight = 10f;

    private void Start()
    {
        // ReSharper disable once PossibleNullReferenceException
        cam = Camera.main.transform;
    }

    private void Update()
    {
        Vector2 camPos = cam.position;
        foreach (Transform bg in bgs)
        {
            bg.Translate(moveDirection * Time.deltaTime);
            
            Vector2 bgPos = bg.position;
            if (bgPos.y - camPos.y - camTop < -30)
            {
                bg.position += 40f * Vector3.up;
            }
            else if (bgPos.y - camPos.y + camTop > 30)
            {
                bg.position += 40f * Vector3.down;
            }
            else if (bgPos.x - camPos.x + camRight > 30)
            {
                bg.position += 40f * Vector3.left;
            }
            else if (bgPos.x - camPos.x - camRight < -30)
            {
                bg.position += 40f * Vector3.right;
            }
        }
    }
}
