using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Singleton = null;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (Singleton == null)
        {
            Singleton = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    //Load in the different levels
    public void SplashScreen()
    {
        SceneManager.LoadScene("");
    }

    //Load main menu
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //Load level 1
    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level_01");
    }

    //Load level 2
    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level_02");
    }

    //Load level 3
    public void LoadLevel3()
    {
        SceneManager.LoadScene("Level_03");
    }

    //Exits the game
    public void ExitGame()
    {
        Application.Quit();
    }
}
