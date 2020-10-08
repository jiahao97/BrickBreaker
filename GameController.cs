using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public Text scoreText, lifeScore, highScoreText, pauseText;
    public Button playBtn, pauseBtn;

    public Sprite[] playSprite;
    public GameObject pausePanel;

    private void Awake()
    {
        instance = this;
    }

    public void NextLevelPanel()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        pauseText.text = "WELL DONE";
        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();

        pauseBtn.interactable = true;
        playBtn.onClick.RemoveAllListeners();
        playBtn.image.sprite = playSprite[0];
        playBtn.onClick.AddListener(() => NextLevel());
    }

    void NextLevel()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 0;

        if(SceneManager.GetActiveScene().name == "Lv1"
            && GameplayController.instance.brickCounter == 0)
        {
            SceneManager.LoadScene("Lv2");
        }
    }
    
    public void Died()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        pauseText.text = "GAME OVER";
        PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("Score"));

        pauseBtn.interactable = true;
        playBtn.onClick.RemoveAllListeners();
        playBtn.image.sprite = playSprite[1];
        playBtn.onClick.AddListener(() => PlayAgain());
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        pauseText.text = "PAUSED";
        PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("Score"));

        pauseBtn.interactable = true;
        playBtn.onClick.RemoveAllListeners();
        playBtn.image.sprite = playSprite[0];
        playBtn.onClick.AddListener(() => Unpause());
    }

    void Unpause()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void PlayAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Gameplay");
    }

    public void Home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }

}
