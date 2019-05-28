using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{
    [SerializeField] private Image icon = null;
    [SerializeField] private TextMeshProUGUI condition = null;
    [SerializeField] private TextMeshProUGUI reward = null;

    public void Init(AchievementScriptable achievement)
    {
        icon.sprite = achievement.icon;
        condition.text = achievement.condition;
        reward.text = achievement.reward;
    }
}
