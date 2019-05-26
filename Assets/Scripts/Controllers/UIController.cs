using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lengthText = null;
    [SerializeField] private TextMeshProUGUI scoreText = null;
    [SerializeField] private GameObject restartText = null;
    [SerializeField] private GameObject continueText = null;
    [SerializeField] private GameObject upgradeText = null;

    private void Start()
    {
        Dragon player = GameObject.FindGameObjectWithTag("Player").GetComponent<Dragon>();
        
        player.OnGrowTail += UpdateStats;
        player.OnGrowTail += CheckUpgrade;

        player.OnDestroyTail += UpdateStats;
        player.OnDestroyTail += DisableUpgrade;

        player.OnDeath += EnableRestart;
    }

    private void UpdateStats(Dragon player)
    {
        lengthText.text = $"LENGTH: {Mathf.Max(0, player.tailSize - 1)} / {player.maxTailSize - 1}";
        scoreText.text = "SCORE: " + player.score;
    }

    private void CheckUpgrade(Dragon player)
    {
        upgradeText.SetActive(player.tailSize > player.tailUpgradeSize);
    }

    private void EnableRestart(Dragon player)
    {
        restartText.SetActive(true);
    }

    public void TogglePause()
    {
        restartText.SetActive(SceneController.isPaused);
        continueText.SetActive(SceneController.isPaused);
    }
    
    private void DisableUpgrade(Dragon player)
    {
        upgradeText.SetActive(false);
    }
}
