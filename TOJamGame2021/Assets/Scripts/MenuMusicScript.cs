using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicScript : MonoBehaviour
{

    //Sounds
    private GameObject soundControllerObject;

    //Song index to play at the menu
    public int songIndex;

    // Start is called before the first frame update
    void Start()
    {
        //Setup the music
        soundControllerObject = GameObject.Find("SoundController");
        soundControllerObject.SendMessage("PlayMusic", songIndex);
    }
}
