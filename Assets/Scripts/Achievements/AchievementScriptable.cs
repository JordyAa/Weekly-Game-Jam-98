using UnityEngine;

[CreateAssetMenu(menuName="Achievement",fileName="New Achievement")]
public class AchievementScriptable : ScriptableObject
{
    public Sprite icon = null;
    public string condition = "CONDITION";
    public string reward = "REWARD";

    public int shotsRequired = 0;
    public int killsRequired = 0;
    public int deathsRequired = 0;
}
