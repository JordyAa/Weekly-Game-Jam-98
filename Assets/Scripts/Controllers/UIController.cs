using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI sizeText = null;
    [SerializeField] private TextMeshProUGUI scoreText = null;
    [SerializeField] private GameObject restartText = null;
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
        sizeText.text = $"SIZE: {Mathf.Max(0, player.tailSize - 1)} / {player.maxTailSize - 1}";
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
    
    private void DisableUpgrade(Dragon player)
    {
        upgradeText.SetActive(false);
    }
}
