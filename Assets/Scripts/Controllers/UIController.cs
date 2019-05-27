using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lengthText = null;
    [SerializeField] private GameObject restartText = null;
    [SerializeField] private GameObject continueText = null;

    private void Start()
    {
        Dragon player = GameObject.Find("Player").GetComponent<Dragon>();
        
        player.OnGrowTail += UpdateStats;
        player.OnDestroyTail += UpdateStats;
        player.OnDeath += EnableRestart;
    }

    private void UpdateStats(Dragon player)
    {
        lengthText.text = "LENGTH: " + player.tails.Count;
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
}
