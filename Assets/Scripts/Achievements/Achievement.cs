using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{
    private Image icon;
    private TextMeshProUGUI condition;
    private TextMeshProUGUI reward;

    private void Start()
    {
        icon = transform.Find("Icon").GetComponent<Image>();
        condition = transform.Find("Condition").GetComponent<TextMeshProUGUI>();
        reward = transform.Find("Reward").GetComponent<TextMeshProUGUI>();
    }

    public void Init(AchievementScriptable achievement)
    {
        icon.sprite = achievement.icon;
        condition.text = achievement.condition;
        reward.text = achievement.reward;
    }
}
