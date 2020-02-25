using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuBall : MonoBehaviour
{
    public float power = 5f;
    public float slowedTime = 0.1f;

    public bool isClicking = false;

    Vector2 firstTouchPos;
    Vector2 launchTouchPos;
    Vector2 direction;
    Touch touch;

    public GameObject line;
    public GameObject gameCanvas;



    Rigidbody2D rb;

    void Start()
    {
        gameCanvas.SetActive(true);
        line.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {       
        if(Input.touchCount>0)
        {
            touch = Input.GetTouch(0);
            if(touch.phase==TouchPhase.Began)
            {
                isClicking = true;
                firstTouchPos = touch.position;
                line.GetComponent<line>().GetFirstTouchPos(firstTouchPos);

            }
        }
        if (isClicking)
        {
            StartCoroutine(Shoot());            
        }

    }
    IEnumerator Shoot()
    {
        if (isClicking)
        {
            Time.timeScale = slowedTime;
            launchTouchPos = touch.position;
            line.GetComponent<line>().GetLaunchTouchPos(launchTouchPos);
            line.SetActive(true);
            FindObjectOfType<line>().DrawLine();
            while (touch.phase != TouchPhase.Ended)
            {
                yield return null;
            }

            Launch();
        }
    }

    void Launch()
    {
        
        direction =   firstTouchPos - launchTouchPos;
        rb.velocity = new Vector2(0, 0);
        rb.velocity = direction * power;


        Time.timeScale = 1f;
        line.SetActive(false);
        isClicking = false;
    }

    
    public void StartButtonTapped()
    {
        gameCanvas.SetActive(false);
        SceneManager.LoadScene(1);
        GameEngine.instance.EnableBoard();

    }
    public void QuitButtonTapped()
    {
        Application.Quit();
    }
    



}
