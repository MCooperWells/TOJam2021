using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesSFX : MonoBehaviour
{
    private AudioSource spikeSFX;

    private void Start()
    {
        spikeSFX = GetComponent<AudioSource>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Spikes")
        {
            PlaySFX();
        }
    }

    private void PlaySFX()
    {
        if(!spikeSFX.isPlaying)
        {
            spikeSFX.Play();
        }
    }

}
