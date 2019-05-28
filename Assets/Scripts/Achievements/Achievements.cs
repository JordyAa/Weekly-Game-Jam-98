using UnityEngine;
using UnityEngine.UI;

public class Achievements : MonoBehaviour
{
    [SerializeField] private Transform achievementHolder = null;
    [SerializeField] private GameObject achievementPrefab = null;
    [SerializeField] private AchievementScriptable[] achievements = new AchievementScriptable[0];
    
    public static int highScore
    {
        get => PlayerPrefs.GetInt("highScore", 0);
        set => PlayerPrefs.SetInt("highScore", Mathf.Max(value, highScore));
    }
    
    public static int shotsFired
    {
        get => PlayerPrefs.GetInt("shotsFired", 0);
        set => PlayerPrefs.SetInt("shotsFired", value);
    }

    public static int totalKills
    {
        get => PlayerPrefs.GetInt("totalKills", 0);
        set => PlayerPrefs.SetInt("totalKills", value);
    }
    
    public static int totalDeaths
    {
        get => PlayerPrefs.GetInt("totalDeaths", 0);
        set => PlayerPrefs.SetInt("totalDeaths", value);
    }

    private void Start()
    {
        if (achievementHolder == null) return;
        
        foreach (AchievementScriptable achievement in achievements)
        {
            GameObject go = Instantiate(achievementPrefab, achievementHolder);
            go.GetComponent<Achievement>().Set(achievement);
            
            if (AchievementScriptable.Unlocked(achievement))
            {
                go.transform.GetChild(0).GetComponent<Image>().color = Color.white;
            }
        }
    }
    
    public int GetProjectiles()
    {
        int shots = shotsFired;
        return shots > achievements[2].shotsRequired ? 4 :
            shots > achievements[1].shotsRequired ? 3 :
            shots > achievements[0].shotsRequired ? 2 : 1;
    }

    public float GetCooldownModifier()
    {
        int kills = totalKills;
        return kills > achievements[7].killsRequired ? 0.75f :
            kills > achievements[6].killsRequired ? 0.80f :
            kills > achievements[5].killsRequired ? 0.85f :
            kills > achievements[4].killsRequired ? 0.90f :
            kills > achievements[3].killsRequired ? 0.95f : 1f;
    }

    public float GetMoveModifier()
    {
        int death = totalDeaths;
        return death > achievements[12].deathsRequired ? 1.25f :
            death > achievements[11].deathsRequired ? 1.20f :
            death > achievements[10].deathsRequired ? 1.15f :
            death > achievements[9].deathsRequired ? 1.10f :
            death > achievements[8].deathsRequired ? 1.05f : 1f;
    }
}
