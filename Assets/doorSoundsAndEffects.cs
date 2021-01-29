using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorSoundsAndEffects : MonoBehaviour
{
    private ParticleSystem leftParticle;
    private ParticleSystem rightParticle;

    private void Start()
    {
        leftParticle = GameObject.Find("DoorEffects").transform.GetChild(0).GetComponent<ParticleSystem>();
        rightParticle = GameObject.Find("DoorEffects").transform.GetChild(1).GetComponent<ParticleSystem>();
    }

    void openDoorSound()
    {
        SoundManagerScript.PlaySound("openDoor");
    }
    void closeDoorSound()
    {
        SoundManagerScript.PlaySound("closeDoor");
        rightParticle.Play();
        leftParticle.Play();
    }
}
