using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{
    public void Set(AchievementScriptable achievement)
    {
        transform.Find("Icon").GetComponent<Image>().sprite = achievement.icon;
        transform.Find("Condition").GetComponent<TextMeshProUGUI>().text = achievement.condition;
        transform.Find("Reward").GetComponent<TextMeshProUGUI>().text = achievement.reward;
    }
}
