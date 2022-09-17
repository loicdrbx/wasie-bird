using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Text scoreText;
    public GameObject playButton;
    public GameObject gameOver;
    private int score;

    public void Awake()
    {
        Application.targetFrameRate = 60;

        Pause();
    }
    public void Play()
    {
        // Reset score
        score = 0;
        scoreText.text = score.ToString();

        // Hide play button and game over image
        playButton.SetActive(false);
        gameOver.SetActive(false);

        // Set time and enable player
        Time.timeScale = 1f;
        player.enabled = true;

        // Destroy pipes
        Pipes[] pipes = FindObjectsOfType<Pipes>();
        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }
    }

    public void Pause()
    {
        // Freeze time
        Time.timeScale = 0f;

        // Disable the player
        player.enabled = false;
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        playButton.SetActive(true);
        Pause();
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
