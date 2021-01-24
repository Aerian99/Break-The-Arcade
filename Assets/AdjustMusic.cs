using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustMusic : MonoBehaviour
{
    public AudioSource audSrc;
    private float musicVolume = 0.5f;

    // Update is called once per frame
    void Update()
    {
        audSrc.volume = musicVolume;
    }

    public void UpdateVolume(float volume)
    {
        musicVolume = volume;
    }

}
