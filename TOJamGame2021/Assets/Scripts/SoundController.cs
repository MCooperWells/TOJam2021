using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{

    /*Two arrays of Audioclips, one for music and one for effects*/
    public AudioClip[] musicSFX;
    public AudioClip[] effectsSFX;

    /*Two different Audio Sources*/
    public AudioSource musicSource;
    public AudioSource effectsSource;

    //I want the sound controller to be a singleton
    public static SoundController Singleton = null;


    private void Awake()
    {
        //Don't destroy this game object
        DontDestroyOnLoad(this.gameObject);

        //Check if this is the 1st one, if it is, then it's the singleton, if its not, then destroy it
        if (Singleton == null)
        {
            Singleton = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    /*If the public PlayEffects function is called in any other script, I want to playOneShot (plays the audio clip only once)
     for which ever soundIndex is specified. In the inspector, I have assigned 16 audio clips for sound effects, but Im sure that
     number will increase with time*/
    public void PlayEffects(int soundIndex)
    {
        effectsSource.PlayOneShot(effectsSFX[soundIndex]);
    }

    /*For the Music, I want to song to play continuously, so I use .Play(). If a new song is requested, then I first stop the current
     one use the Stop() function, then change the audio clip, then play it*/
    public void PlayMusic(int soundIndex)
    {
        musicSource.Stop();
        musicSource.clip = musicSFX[soundIndex];
        musicSource.Play();
    }

    //Short function to Stop the music if I ever need to (Like if Kirby Dies)
    public void StopMusic()
    {
        musicSource.Stop();
    }


}
