using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int lives;
    public int score;
    public Text livesText;
    public Text scoreText;
    public Text highScoreText;
    public InputField highScoreInput;
    public bool gameOver;
    public GameObject gameOverPanel;
    public GameObject loadLevelPanel;
    public int bricksNo;
    public Transform[] levels;
    public int currentLevel = 0;


    // Start is called before the first frame update
    void Start()
    {
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;
        bricksNo = GameObject.FindGameObjectsWithTag("Brick").Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLives(int changeInLives)
    {
        lives += changeInLives;
        //check for no lives left trigger end of game
        if(lives <= 0)
        {
            lives = 0;
            GameOver();
        }
        livesText.text = "Lives: " + lives;
    }

    public void UpdateScore(int changeInScore)
    {
        score += changeInScore;
        scoreText.text = "Score: " + score;
    }

    public void UpdateNumberOfBricks()
    {
        bricksNo--;
        if(bricksNo <= 0)
        {
            if(currentLevel >= levels.Length - 1)
            {
                GameOver();
            }
            else
            {
                loadLevelPanel.SetActive(true);
                loadLevelPanel.GetComponentInChildren<Text>().text = " Loading Level " + (currentLevel + 2);
                gameOver = false;
                Invoke("LoadLevel", 3.5f);
            }
        }
    }

    private void LoadLevel()
    {
        currentLevel++;
        Instantiate(levels[currentLevel], Vector2.zero, Quaternion.identity);
        bricksNo = GameObject.FindGameObjectsWithTag("Brick").Length;
        gameOver = false;
        loadLevelPanel.SetActive(false);

    }

    private void GameOver()
    {
        gameOver = true;
        gameOverPanel.SetActive(true);
        int highScore = PlayerPrefs.GetInt("HIGHSCORE");
        if(score > highScore)
        {
            PlayerPrefs.SetInt("HIGHSCORE", score);
            highScoreText.text = "New High Score!" + "\n" + "Enter Your Name Below.";
            highScoreInput.gameObject.SetActive(true);
        }
        else
        {
            highScoreText.text = PlayerPrefs.GetString("HIGHSCORENAME") + "'s High Score was " + highScore + "\n" + "Can you beat it?";

        }
    }

    public void NewHighScore()
    {
        string name = highScoreInput.text;
        PlayerPrefs.SetString("HIGHSCORENAME", name);
        highScoreInput.gameObject.SetActive(false);
        highScoreText.text = "Congratulations " + name + "!" + "\n" + "Your New High Score is " + score;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
