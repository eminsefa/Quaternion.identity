using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public float slowedTime = 0.1f;

    public bool isClicking = false;

    
  

   
   
   

    
    public void StartButtonTapped()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitButtonTapped()
    {
        Application.Quit();
    }
}
