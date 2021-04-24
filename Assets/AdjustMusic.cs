using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustMusic : MonoBehaviour
{

    private void Start()
    {
        UpdateVolume(0.5f);
    }
    public void UpdateVolume(float volume)
    {
        AudioListener.volume = volume;
    }
    
}
