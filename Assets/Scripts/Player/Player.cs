using UnityEngine;

public class Player : MonoBehaviour
{
    public static int highScore { get; private set; }
    
    private void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore");
        GameObject.Find("Player").GetComponent<Dragon>().OnGrowTail += CheckHighScore;
    }

    private static void CheckHighScore(Dragon dragon)
    {
        if (dragon.tails.Count > highScore)
        {
            Debug.Log("New High Score");
            highScore = dragon.tails.Count;
            PlayerPrefs.SetInt("highScore", highScore);
            PlayerPrefs.Save();
        }
    }
}
