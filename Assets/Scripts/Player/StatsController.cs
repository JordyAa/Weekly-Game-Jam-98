using UnityEngine;

public class StatsController : MonoBehaviour
{
    public static int highScore { get; private set; }
    
    private void Awake()
    {
        highScore = PlayerPrefs.GetInt("highScore");
    }

    private void Start()
    {
        GameObject go = GameObject.Find("Player");
        if (go != null)
        {
            go.GetComponent<Dragon>().OnGrowTail += CheckHighScore;
        }
    }

    private static void CheckHighScore(Dragon dragon)
    {
        if (dragon.tails.Count > highScore)
        {
            highScore = dragon.tails.Count;
            PlayerPrefs.SetInt("highScore", highScore);
            PlayerPrefs.Save();
        }
    }
}
