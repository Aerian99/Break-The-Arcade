using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustMusic : MonoBehaviour
{
     public void UpdateVolume(float volume)
    {
        AudioListener.volume = volume;
    }
    
}
