using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float power = 10f;
    public float slowedTime = 0.1f;


    Vector2 firstTouchPos;
    Vector2 launchTouchPos;
    Vector2 direction;
    Touch touch;


    public GameObject line;

    public GameObject lifePrefab;
    GameObject[] lives;

    public int numberOfLives = 5;


    Rigidbody2D rb;
    

    void Start()
    {
        GameEngine.instance.EnableBoard();
        line.SetActive(false);
        rb = GetComponent<Rigidbody2D>();

        lives = new GameObject[numberOfLives];
        for (int i=0; i<numberOfLives; i++)
        {
            Vector2 livesPosition= new Vector2(2.5f + i * 0.3f, 0.15f);
            GameObject life = Instantiate(lifePrefab, livesPosition, Quaternion.identity);
            lives[i] = life;
        }
        
    }

    void Update()
    {
        
        if (!GameEngine.instance.isClicking && GameEngine.instance.timerStarted)
        {
            GameEngine.instance.AddNormalTime();
        }
        
        if (Input.touchCount > 0)
        {
            
            touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Began && GameEngine.instance.isClicking)
            {                
                numberOfLives--;
                firstTouchPos = touch.position;
                line.GetComponent<line>().GetFirstTouchPos(firstTouchPos);
            }
            
            if (GameEngine.instance.isClicking)
            {
                StartCoroutine(Shoot());
                if (GameEngine.instance.timerStarted)
                {
                    GameEngine.instance.AddFastenedTime();
                }
            }            
        }
        
        
        
        
        
    }
    IEnumerator Shoot()
    {
        
            Time.timeScale = slowedTime;
            launchTouchPos = touch.position;
            line.GetComponent<line>().GetLaunchTouchPos(launchTouchPos);
            line.SetActive(true);
            line.GetComponent<line>().DrawLine();

        while (touch.phase != TouchPhase.Ended)
            {
                yield return null;
            }
            Launch();
    }
        
    void Launch()
    {
        direction = firstTouchPos - launchTouchPos;
        

        rb.velocity = new Vector2(0, 0);
        rb.velocity = direction * power;
        

        Time.timeScale = 1f;
        GameEngine.instance.StartTimer();
        line.SetActive(false);
        GameEngine.instance.isClicking = false;

        
        Destroy(lives[numberOfLives].gameObject);
        if (numberOfLives <= 0)
        {
            GameEngine.instance.DisableBoard();
            return;
        }





    }
  
 

    

}
