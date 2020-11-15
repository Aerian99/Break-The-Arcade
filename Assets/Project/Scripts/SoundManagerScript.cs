using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip jumpSound,
        shotgunSound,
        landingSound,
        yellowgunSound,
        purpleGunSound,
        dashSound,
        platformJump,
        gameOver;

    static AudioSource audioSrc;

    void Start()
    {
        jumpSound = Resources.Load<AudioClip>("jump");
        shotgunSound = Resources.Load<AudioClip>("shotgun");
        landingSound = Resources.Load<AudioClip>("landing");
        yellowgunSound = Resources.Load<AudioClip>("yellowGun");
        purpleGunSound = Resources.Load<AudioClip>("purpleGun");
        dashSound = Resources.Load<AudioClip>("dash");
        platformJump = Resources.Load<AudioClip>("platformJump");
        gameOver = Resources.Load<AudioClip>("gameOver");


        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "jump":
                audioSrc.PlayOneShot(jumpSound);
                break;
            case "shotgun":
                audioSrc.PlayOneShot(shotgunSound);
                break;
            case "landing":
                audioSrc.PlayOneShot(landingSound);
                break;
            case "yellowGun":
                audioSrc.PlayOneShot(yellowgunSound);
                break;
            case "purpleGun":
                audioSrc.PlayOneShot(purpleGunSound);
                break;
            case "dash":
                audioSrc.PlayOneShot(dashSound);
                break;
            case "platformJump":
                audioSrc.PlayOneShot(platformJump);
                break;
            case "gameOver":
                audioSrc.PlayOneShot(gameOver);
                break;

            default:
                break;
        }
    }
}