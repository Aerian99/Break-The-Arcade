using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorSounds : MonoBehaviour
{
    
    void openDoorSound()
    {
        SoundManagerScript.PlaySound("openDoor");
    }
    void closeDoorSound()
    {
        SoundManagerScript.PlaySound("closeDoor");
    }
}
