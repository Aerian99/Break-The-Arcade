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
        gameOver,
        EnemyShoot,
        noAmmo,
        alienExplosion,
        openDoor,
        closeDoor,
        footStep,
        absorbSound;

    public static AudioSource audioSrc;

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
        EnemyShoot = Resources.Load<AudioClip>("EnemyShoot");
        noAmmo = Resources.Load<AudioClip>("noAmmo");
        alienExplosion = Resources.Load<AudioClip>("alienExplosion");
        openDoor = Resources.Load<AudioClip>("openDoor");
        closeDoor = Resources.Load<AudioClip>("closeDoor");
        footStep = Resources.Load<AudioClip>("footStep");
        absorbSound = Resources.Load<AudioClip>("absorbSound");


        audioSrc = GetComponent<AudioSource>();
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
            case "EnemyShoot":
                audioSrc.PlayOneShot(EnemyShoot);
                break;
            case "noAmmo":
                audioSrc.PlayOneShot(noAmmo);
                break;
            case "alienExplosion":
                audioSrc.PlayOneShot(alienExplosion);
                break;
            case "openDoor":
                audioSrc.PlayOneShot(openDoor);
                break;
            case "closeDoor":
                audioSrc.PlayOneShot(closeDoor);
                break;
            case "footStep":
                audioSrc.PlayOneShot(footStep);
                break;
            case "absorbSound":
                audioSrc.PlayOneShot(absorbSound);
                break;
            default:
                break;
        }
    }
}