using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScreenManager : MonoBehaviour
{
    //Blackscreen for fade in fade out
    public Image blackScreen;

    //Colour used for the screen fade
    private Color colour;

    //Images to be displayed
    public GameObject TOJamOfficialImage;
    public GameObject backgroundNameImage;
    public GameObject gameNameImage;

    //Fade rate
    public float radeRate;

    //Timer
    private float timer;
    
    //Name of the main menu level to be loaded
    public string mainMenu;


    //Sounds
    private GameObject soundControllerObject;

    //Song index to play at the menu
    public int songIndex;

    // Start is called before the first frame update
    void Start()
    {
        //Start with a black screen
        colour = blackScreen.color;
        colour.a = 1;
        blackScreen.color = colour;
        
        //Show the TOJam first  
        ShowGameBackgroundImage(false);
        ShowTOJamOfficial(true);
        ShowGameNameImage(false);

        //Setup the music
        soundControllerObject = GameObject.Find("SoundController");
        soundControllerObject.SendMessage("PlayMusic", songIndex);
    }

    // Update is called once per frame
    void Update()
    {
        //Add to the timer
        timer += Time.deltaTime;

        if(timer > 12f)
        {
            SceneManager.LoadScene(mainMenu);
        }
        else if(timer > 7f)
        {
            ShowGameNameImage(true);
        }
        else if(timer > 6f)
        {
            colour.a -= radeRate * 20;
            blackScreen.color = colour;

            if(backgroundNameImage.activeSelf == false)
            {
                ShowGameBackgroundImage(true);
                ShowTOJamOfficial(false);
            }
        }
        else if(timer > 3f)
        {
            colour.a += radeRate * 10;
            blackScreen.color = colour;
        }   
        else if (timer > 0f)
        {
            colour.a -= radeRate * 10;
            blackScreen.color = colour;
        }
    }


    void ShowGameBackgroundImage(bool show)
    {
        backgroundNameImage.SetActive(show);
    }
    void ShowTOJamOfficial(bool show)
    {
        TOJamOfficialImage.SetActive(show);
    }
    void ShowGameNameImage(bool show)
    {
        gameNameImage.SetActive(show);
    }
}
