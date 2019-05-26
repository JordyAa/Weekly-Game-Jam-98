using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI sizeText = null;
    [SerializeField] private TextMeshProUGUI scoreText = null;
    [SerializeField] private GameObject upgradeText = null;

    private void Start()
    {
        Player.OnGrowTail += UpdateStats;
        Player.OnGrowTail += CheckUpgrade;

        Player.OnDestroyTail += UpdateStats;
        Player.OnDestroyTail += DisableUpgrade;
    }

    private void UpdateStats(Player player)
    {
        sizeText.text = $"SIZE: {player.tailSize - 1} / 15";
        scoreText.text = "SCORE: " + player.score;
    }

    private void CheckUpgrade(Player player)
    {
        upgradeText.SetActive(player.tailSize > 10);
    }

    private void DisableUpgrade(Player player)
    {
        upgradeText.SetActive(false);
    }
}
