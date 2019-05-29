using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreText = null;

    [SerializeField] private Image toggleImage = null;
    [SerializeField] private Sprite muteIcon = null;
    [SerializeField] private Sprite unmuteIcon = null;
    
    private void Start()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "HIGH SCORE: " + Achievements.highScore;
        }
        
        if (toggleImage != null)
        {
            AudioSource music = GameObject.Find("AudioController").GetComponent<AudioSource>();
            toggleImage.sprite = music.enabled ? muteIcon : unmuteIcon;
        }
    }

    public void ToggleMusic()
    {
        AudioSource music = GameObject.Find("AudioController").GetComponent<AudioSource>();
        bool e = music.enabled;
        e = !e;
        music.enabled = e;
        toggleImage.sprite = e ? muteIcon : unmuteIcon;
    }

    public void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
