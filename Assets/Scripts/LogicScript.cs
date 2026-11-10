using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
public class LogicScript : MonoBehaviour
{
    [Header("Sound Effects")]
    public AudioSource audioSource;

    public AudioClip buttonSound;
    public AudioClip birdFlapSound;
    public AudioClip gameOverSound;

    [Header("High Score UI")]
    public GameObject highScorePanel;

    public TMP_Text rank1ScoreText;
    public TMP_Text rank2ScoreText;
    public TMP_Text rank3ScoreText;
    public int playerScore;
    public Text scoreText;
    public GameObject gameOverScreen;
    public bool isGameOver = false;
    public bool isGameStarted = false;
    public GameObject gameBeginCanvas;
    [ContextMenu("Increase score")]
    public void addScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        scoreText.text = playerScore.ToString();
    }

    public void restartGame()
    {
        PlayButtonSound();

        StartCoroutine(RestartAfterSound());
    }

    IEnumerator RestartAfterSound()
    {
        yield return new WaitForSecondsRealtime(buttonSound.length);

        Time.timeScale = 1;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
    }

    public void gameOver()
    {
        if (isGameOver == true)
        {
            return;
        }

        isGameOver = true;

        // Lưu điểm vào bảng Top 3
        SaveHighScore(playerScore);

        PlayGameOverSound();

        // Hiển thị Game Over
        gameOverScreen.SetActive(true);

        // Dừng game
        Time.timeScale = 0;
    }

    void Awake()
    {
        Time.timeScale = 0;
    }

    public void startGame()
    {
        PlayButtonSound();

        isGameStarted = true;

        gameBeginCanvas.SetActive(false);

        Time.timeScale = 1;
    }

    public void UpdateHighScoreUI()
    {
        rank1ScoreText.text =
            PlayerPrefs.GetInt("HighScore1", 0).ToString();

        rank2ScoreText.text =
            PlayerPrefs.GetInt("HighScore2", 0).ToString();

        rank3ScoreText.text =
            PlayerPrefs.GetInt("HighScore3", 0).ToString();
    }

    public void ShowHighScores()
    {
        PlayButtonSound();
        UpdateHighScoreUI();
        highScorePanel.SetActive(true);
    }

    public void HideHighScores()
    {
        highScorePanel.SetActive(false);
    }

    public void SaveHighScore(int newScore)
    {
        int score1 = PlayerPrefs.GetInt("HighScore1", 0);
        int score2 = PlayerPrefs.GetInt("HighScore2", 0);
        int score3 = PlayerPrefs.GetInt("HighScore3", 0);

        if (newScore > score1)
        {
            score3 = score2;
            score2 = score1;
            score1 = newScore;
        }
        else if (newScore > score2)
        {
            score3 = score2;
            score2 = newScore;
        }
        else if (newScore > score3)
        {
            score3 = newScore;
        }

        PlayerPrefs.SetInt("HighScore1", score1);
        PlayerPrefs.SetInt("HighScore2", score2);
        PlayerPrefs.SetInt("HighScore3", score3);

        PlayerPrefs.Save();
    }

    public void PlayButtonSound()
    {
        audioSource.PlayOneShot(buttonSound);
    }

    public void PlayFlapSound()
    {
        audioSource.PlayOneShot(birdFlapSound, 0.5f);
    }

    public void PlayGameOverSound()
    {
        audioSource.PlayOneShot(gameOverSound);
    }
}
