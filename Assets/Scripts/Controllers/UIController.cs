using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lengthText = null;
    [SerializeField] private TextMeshProUGUI highScoreText = null;
    [SerializeField] private GameObject continueText = null;
    [SerializeField] private GameObject restartText = null;
    [SerializeField] private GameObject menuText = null;
    
    private void Start()
    {
        Dragon player = GameObject.Find("Player").GetComponent<Dragon>();
        UpdateStats(player);
        
        player.OnGrowTail += UpdateStats;
        player.OnDestroyTail += UpdateStats;
        player.OnDeath += EnableRestart;
    }

    private void UpdateStats(Dragon player)
    {
        lengthText.text = "LENGTH: " + player.tails.Count;
        highScoreText.text = "HIGH SCORE: " + Achievements.highScore;
    }

    private void EnableRestart(Dragon player)
    {
        highScoreText.enabled = true;
        restartText.SetActive(true);
        menuText.SetActive(true);
    }

    public void TogglePause()
    {
        highScoreText.enabled = SceneController.isPaused;
        restartText.SetActive(SceneController.isPaused);
        continueText.SetActive(SceneController.isPaused);
        menuText.SetActive(SceneController.isPaused);
    }
}
