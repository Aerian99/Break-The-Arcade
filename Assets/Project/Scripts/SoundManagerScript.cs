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
        absorbSound,
        absorbBlip,
        hurt,
        powerup,
        radialEnemyHurt,
        radialEnemyDeath,
        robotDeath,
        radialShoot;

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
        absorbBlip = Resources.Load<AudioClip>("absorbBlip");
        hurt = Resources.Load<AudioClip>("hurt");
        powerup = Resources.Load<AudioClip>("powerup");
        radialEnemyHurt = Resources.Load<AudioClip>("radialEnemyHurt");
        radialEnemyDeath = Resources.Load<AudioClip>("radialEnemyDeath");
        robotDeath = Resources.Load<AudioClip>("robotDeath");
        radialShoot = Resources.Load<AudioClip>("radialShoot");

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
            case "absorbBlip":
                audioSrc.PlayOneShot(absorbBlip);
                break;
            case "hurt":
                audioSrc.PlayOneShot(hurt);
                break;
            case "powerup":
                audioSrc.PlayOneShot(powerup);
                break;
            case "radialEnemyHurt":
                audioSrc.PlayOneShot(radialEnemyHurt);
                break;
            case "radialEnemyDeath":
                audioSrc.PlayOneShot(radialEnemyDeath);
                break;
            case "robotDeath":
                audioSrc.PlayOneShot(robotDeath);
                break;
            default:
                break;
        }
    }
}