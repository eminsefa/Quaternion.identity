using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameEngine : MonoBehaviour
{
    public static GameEngine instance;

    public Slider timeSlider;
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public GameObject levelCompletePanel;
    public GameObject gameCanvas;
    public Text scoreText;


    public GameObject board;

    public float realTimer;
    public float slowedTime = 0.1f;

    public bool timerStarted = false;
    public bool isClicking = false;

    public float score = 0;
    public float numberOfCoins;


    void Awake()
    {

        Time.timeScale = 1;
        if(instance==null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            
        }
        else
        {
            Destroy(gameObject);
            
        }


    }
   

    void FixedUpdate()
    {
        score = Mathf.Floor(100 - realTimer);
        numberOfCoins = FindObjectsOfType<Coin>().Length;
        if (numberOfCoins <= 0)
        {
            LevelComplete();
        }

    }
    public void LevelComplete()
    {
        Time.timeScale = 0;
        levelCompletePanel.SetActive(true);
        scoreText.text = "Time Score:"+score.ToString();
    }
    public bool StartTimer()
    {
        return timerStarted = true;
    }
    public void AddNormalTime()
    {
        realTimer += Time.deltaTime * 4;
        timeSlider.GetComponent<Slider>().value = realTimer;
        if (timeSlider.GetComponent<Slider>().value >= 100)
        {
            gameOverPanel.SetActive(true);
            DisableBoard();
            FindObjectOfType<Ball>().GetComponent<Ball>().line.SetActive(false);
            Time.timeScale = 0;
        }

    }
    public void AddFastenedTime()
    {
        realTimer += Time.deltaTime / (slowedTime*slowedTime*4);
        timeSlider.GetComponent<Slider>().value = realTimer;
        if (timeSlider.GetComponent<Slider>().value >= 100)
        {
            gameOverPanel.SetActive(true);
            DisableBoard();
            FindObjectOfType<Ball>().GetComponent<Ball>().line.SetActive(false);
            Time.timeScale = 0;
        }
    }
     
    void ResetData()
    {
        numberOfCoins = FindObjectsOfType<Coin>().Length;
        levelCompletePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
    }
    
    public void PauseButtonTapped()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        DisableBoard();
    }
    public void ResumeButtonTapped()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        EnableBoard();


    }
    public void RestartButtonTapped()
    {
        ResetData(); 
        realTimer = 0;
        timeSlider.GetComponent<Slider>().value = realTimer;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        timerStarted = false;

    }
    public void DisableBoard()
    {
        board.GetComponent<BoxCollider2D>().enabled = false;
    }
    public void EnableBoard()
    {
        if(FindObjectOfType<Ball>().GetComponent<Ball>().numberOfLives<=0)
        {
            return;
        }
        board.GetComponent<BoxCollider2D>().enabled=true;
    }
    public void MainMenuButtonTapped()
    {
        Destroy(gameCanvas);
        Destroy(board.gameObject);
        Destroy(this.gameObject);
        ResetData();
        SceneManager.LoadScene(0);
        EnableBoard();
    }
    public void NextLevelButtonTapped()
    {
        ResetData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        
    }
}
