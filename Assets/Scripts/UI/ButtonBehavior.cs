using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehavior : MonoBehaviour
{
    public AudioSource buttonNoise;
    // Start is called before the first frame update
    public void StartButton()
    {
        SceneManager.LoadScene(1);
        buttonNoise.Play();
    }

    public void QuitButton()
    {
        buttonNoise.Play();
        Application.Quit();

    }

    public void ReadyP1Button()
    {
 
       SceneManager.LoadScene(2);
        buttonNoise.Play();

    }
    public void playAgain()
    {
        SceneManager.LoadScene(2);
        buttonNoise.Play();
    }

    public void titleScreen()
    {
        SceneManager.LoadScene(0);
        buttonNoise.Play();
    }


}
