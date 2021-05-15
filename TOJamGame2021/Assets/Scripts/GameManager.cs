using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int gameLevel = 1;
    public static GameManager Singleton = null;
    private static int gameScore = 0;

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
        gameLevel = 1;
        gameScore = 0;
        SceneManager.LoadScene("Level_01");
    }

    //Load level 2
    public void LoadLevel2()
    {
        gameLevel = 2;
        gameScore = 0;
        SceneManager.LoadScene("Level_01");
    }

    //Load level 3
    public void LoadLevel3()
    {
        gameLevel = 3;
        gameScore = 0;
        SceneManager.LoadScene("Level_01");
    }

    public void RestartLevel()
    {
        Debug.Log("Restarting Level");
        gameScore = 0;
        SceneManager.LoadScene("Level_01");
    }

    public void UpdateLevel()
    {
        if(gameLevel < 3)
        {
            gameLevel++;
        }    
    }

    //Exits the game
    public void ExitGame()
    {
        Application.Quit();
    }

    public void AddToGameScore(int amountGained)
    {
        gameScore += amountGained;
    }

    public int GetGameScore()
    {
        return gameScore;
    }
}
