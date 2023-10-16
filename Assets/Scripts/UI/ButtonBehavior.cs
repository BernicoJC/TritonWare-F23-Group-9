using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehavior : MonoBehaviour
{

    // Start is called before the first frame update
    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void ReadyP1Button()
    {
 
       SceneManager.LoadScene(2);
   
    }
    public void playAgain()
    {
        SceneManager.LoadScene(2);
    }

    public void titleScreen()
    {
        SceneManager.LoadScene(0);
    }


}
