using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LogicScript : MonoBehaviour
{
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
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        if (isGameOver == true)
        {
            return;
        }

        isGameOver = true;
        gameOverScreen.SetActive(true);
    }

    void Awake()
    {
        Time.timeScale = 0;
    }

    public void startGame()
    {
        isGameStarted = true;

        gameBeginCanvas.SetActive(false);

        Time.timeScale = 1;
    }
}
